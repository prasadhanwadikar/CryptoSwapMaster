using BinanceApiAdapter;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace BotsManagerService
{
    public partial class BotsManager : ServiceBase
    {
        private Task _botsManager;
        private CancellationTokenSource _cancelTokenSource;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BotsManager));
        
        public BotsManager()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        public void Start()
        {
            try
            {
                _cancelTokenSource = new CancellationTokenSource();
                _botsManager = new Task(() => ManageBots(), _cancelTokenSource.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.None);
                _botsManager.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("BotsManager failed to start. Error:  " + ex.Message, ex);
            }
        }

        private void ManageBots()
        {
            Thread.CurrentThread.Name = "BotsManager";
            var bots = new Dictionary<int, Bot>();

            try
            {
                _logger.Info("BotsManager started");
                var db = new Repository();

                while (!_cancelTokenSource.Token.IsCancellationRequested)
                {
                    Thread.Sleep(1000);

                    var users = db.GetUsers();

                    foreach (var user in users.Where(u => u.BotStatus == BotStatus.StartRequested || u.BotStatus == BotStatus.Running))
                    {
                        if (bots.ContainsKey(user.Id))
                        {
                            if (user.BotStatus == BotStatus.StartRequested) db.UpdateBotStatus(user.Id, BotStatus.Running);
                            continue;
                        }
                        var bot = new Bot {User = user, CTS = new CancellationTokenSource()};
                        bot.Task = new Task((b) => RunBot((Bot) b), bot, bot.CTS.Token, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.None);
                        bot.Task.Start();
                        bots.Add(user.Id, bot);
                    }

                    foreach (var user in users.Where(u => u.BotStatus == BotStatus.StopRequested))
                    {
                        if (!bots.ContainsKey(user.Id))
                        {
                            db.UpdateBotStatus(user.Id, BotStatus.Stopped);
                            continue;
                        }
                        bots[user.Id].CTS.Cancel();
                        bots[user.Id].Task.Wait();
                        bots.Remove(user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error: " + ex.Message, ex);
            }
            finally
            {
                foreach (var bot in bots)
                {
                    bot.Value.CTS.Cancel();
                }
                Task.WaitAll(bots.Values.Select(x => x.Task).ToArray());
                _logger.Info("BotsManager stopped");
            }
        }

        private void RunBot(Bot b)
        {
            var db = new Repository();
            try
            {
                Thread.CurrentThread.Name = "Bot" + b.User.Id;
                var binance = new BinanceApiClient(b.User.ApiKey, b.User.SecretKey);

                db.UpdateBotStatus(b.User.Id, BotStatus.Running);

                _logger.Info(Thread.CurrentThread.Name + " started");

                while (!b.CTS.IsCancellationRequested)
                {
                    var orders = db.GetOrders(b.User.Id);
                    foreach (var order in orders)
                    {
                        
                    }
                }

                db.UpdateBotStatus(b.User.Id, BotStatus.Stopped);
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error("Error: " + ex.Message, ex);
                    db.UpdateBotStatus(b.User.Id, BotStatus.Stopped, ex.Message);
                }
                catch (Exception ex2)
                {
                    _logger.Error("Failed to update bot status because of error: " + ex2.Message, ex2);
                }
            }
            finally
            {
                _logger.Info(Thread.CurrentThread.Name + " stopped");
            }
        }

        protected override void OnStop()
        {
            Stop();
        }

        public new void Stop()
        {
            try
            {
                _cancelTokenSource.Cancel();
                _botsManager.Wait();
            }
            catch (Exception ex)
            {
                _logger.Error("BotsManager failed to stop. Error:  " + ex.Message, ex);
            }
        }
    }
}
