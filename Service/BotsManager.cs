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

namespace BinanceService
{
    public partial class BotsManager : ServiceBase
    {
        private Task _botsManager;
        private CancellationTokenSource _cancelTokenSource;
        private readonly ILog _logger = LogManager.GetLogger("BotsManager");
        
        public BotsManager()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _cancelTokenSource = new CancellationTokenSource();
                _botsManager = new Task(() => ManageBots(), _cancelTokenSource.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.None);
                _botsManager.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("BotsManager failed to start. Error:  " + ex.Message);
            }
        }

        private void ManageBots()
        {
            var bots = new Dictionary<int, Bot>();

            try
            {
                var db = new Repository();

                while (!_cancelTokenSource.IsCancellationRequested)
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
                _logger.Error("BotsManager stopped with error: " + ex.Message);
            }
            finally
            {
                foreach (var bot in bots)
                {
                    bot.Value.CTS.Cancel();
                }
                Task.WaitAll(bots.Values.Select(x => x.Task).ToArray());
            }
        }

        private void RunBot(Bot b)
        {
            var db = new Repository();
            var binance = new BinanceApiClient(b.User.ApiKey, b.User.SecretKey);

            try
            {
                db.UpdateBotStatus(b.User.Id, BotStatus.Running);

                while (!b.CTS.IsCancellationRequested)
                {
                    Thread.Sleep(3000);
                }

                db.UpdateBotStatus(b.User.Id, BotStatus.Stopped);
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error("Bot " + b.User.Id + " stopped with error: " + ex.Message);
                    db.UpdateBotStatus(b.User.Id, BotStatus.Stopped, ex.Message);
                }
                catch (Exception ex2)
                {
                    _logger.Error("Bot " + b.User.Id + " stopped but failed to update its status because of error: " + ex2.Message);
                }
            }
        }

        protected override void OnStop()
        {
            try
            {
                _cancelTokenSource.Cancel();
                _botsManager.Wait();
            }
            catch (Exception ex)
            {
                _logger.Error("BotsManager failed to stop. Error:  " + ex.Message);
            }
        }
    }
}
