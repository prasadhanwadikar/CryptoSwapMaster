using CryptoSwapMaster.BinanceApiAdapter;
using CryptoSwapMaster.Data;
using CryptoSwapMaster.Data.Entities;
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
using CryptoSwapMaster.BinanceApiAdapter.Types;
using log4net;
using log4net.Config;
using CryptoSwapMaster.Data.Enums;
using CryptoSwapMaster.BinanceApiAdapter.Enums;

namespace CryptoSwapMaster.Service
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
                        if (!bots.ContainsKey(user.Id))
                        {
                            var bot = new Bot
                            {
                                User = user,
                                CTS = new CancellationTokenSource(),
                                Binance = new BinanceApiClient(user.ApiKey, user.SecretKey, 30000, 5000)
                            };
                            bot.Observer = new Task((b) => ObserveOrders((Bot)b), bot, bot.CTS.Token, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.None);
                            bot.Processor = new Task((b) => ProcessOrders((Bot)b), bot, bot.CTS.Token, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning | TaskCreationOptions.None);
                            bot.Observer.Start();
                            bot.Processor.Start();
                            bots.Add(user.Id, bot);
                        }

                        if (user.BotStatus != BotStatus.Running) db.UpdateBotStatus(user.Id, BotStatus.Running);
                    }

                    foreach (var user in users.Where(u => u.BotStatus == BotStatus.StopRequested || u.BotStatus == BotStatus.Stopped))
                    {
                        if (bots.ContainsKey(user.Id))
                        {
                            bots[user.Id].CTS.Cancel();
                            if (bots[user.Id].Observer != null) bots[user.Id].Observer.Wait();
                            if (bots[user.Id].Processor != null) bots[user.Id].Processor.Wait();
                            bots.Remove(user.Id);
                        }

                        if (user.BotStatus != BotStatus.Stopped) db.UpdateBotStatus(user.Id, BotStatus.Stopped);
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
                Task.WaitAll(bots.Values.Select(x => x.Observer).ToArray());
                Task.WaitAll(bots.Values.Select(x => x.Processor).ToArray());
                _logger.Info("BotsManager stopped");
            }
        }

        private void ObserveOrders(Bot b)
        {
            var db = new Repository();
            try
            {
                Thread.CurrentThread.Name = "O" + b.User.Id;
                _logger.Info(Thread.CurrentThread.Name + " started");

                while (!b.CTS.IsCancellationRequested)
                {
                    var baseAssetOrdersGroups = db.GetOrders(b.User.Id, OrderStatus.Open).GroupBy(x => x.BaseAsset);

                    if (!baseAssetOrdersGroups.Any())
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    var accountInfo = b.Binance.AccountInfo;

                    foreach (var baseAssetOrdersGroup in baseAssetOrdersGroups)
                    {
                        var assetPoolsSum = baseAssetOrdersGroup.GroupBy(x => x.Pool)
                            .Sum(x => x.GroupBy(y => y.Group).Max(y => y.Sum(z => z.BaseQty)));
                        var assetFreeQty = accountInfo.Balances.First(x => x.Asset == baseAssetOrdersGroup.Key).Free;
                        if (assetFreeQty < assetPoolsSum)
                        {
                            db.CancelOpenOrders(b.User.Id, baseAssetOrdersGroup.Key);
                            continue;
                        }

                        var poolOrdersGroups = baseAssetOrdersGroup.GroupBy(x => x.Pool);
                        Parallel.ForEach(poolOrdersGroups, (poolOrdersGroup) =>
                        {
                            IGrouping<int, Order> selectedGroup = null;
                            var groupOrdersGroups = poolOrdersGroup.GroupBy(x => x.Group);
                            foreach (var groupOrdersGroup in groupOrdersGroups)
                            {
                                var selectGroup = true;
                                foreach (var order in groupOrdersGroup)
                                {
                                    var quote = b.Binance.GetQuote(order.BaseAsset, order.QuoteAsset, order.BaseQty, accountInfo.TakerCommission);
                                    if (quote < order.ExpectedQuoteQty)
                                    {
                                        selectGroup = false;
                                        break;
                                    }
                                }
                                if (selectGroup)
                                {
                                    selectedGroup = groupOrdersGroup;
                                    break;
                                }
                            }

                            if (selectedGroup != null)
                            {
                                var exchangeOrders = new List<ExchangeOrder>();

                                foreach (var order in selectedGroup)
                                {
                                    var orderParts = b.Binance.BuildOrders(order.BaseAsset, order.QuoteAsset, order.BaseQty, accountInfo.TakerCommission);
                                    var i = 1;
                                    foreach (var orderPart in orderParts)
                                    {
                                        var exchangeOrder = new ExchangeOrder
                                        {
                                            OrderId = order.Id,
                                            Sequence = i,
                                            Symbol = orderPart.Symbol,
                                            Side = orderPart.Side.ToString(),
                                            BaseQty = orderPart.OrigQty,
                                            Status = OrderStatus.Open,
                                            Created = DateTime.Now
                                        };
                                        exchangeOrders.Add(exchangeOrder);
                                        i++;
                                    }
                                }

                                db.SaveExchangeOrders(exchangeOrders);
                                db.MarkGroupOrdersInProcess(b.User.Id, selectedGroup.First().BaseAsset, selectedGroup.First().Pool, selectedGroup.First().Group);
                            }
                        });
                    }
                }

                db.UpdateBotStatus(b.User.Id, BotStatus.Stopped);
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error("Error: " + ex.Message, ex);
                    db.UpdateBotStatus(b.User.Id, BotStatus.Stopped, "Stopped due to error");
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

        private void ProcessOrders(Bot b)
        {
            var db = new Repository();
            try
            {
                Thread.CurrentThread.Name = "P" + b.User.Id;
                _logger.Info(Thread.CurrentThread.Name + " started");

                while (!b.CTS.IsCancellationRequested)
                {
                    var orders = db.GetOrders(b.User.Id, OrderStatus.InProcess);

                    if (!orders.Any())
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    var accountInfo = b.Binance.AccountInfo;

                    Parallel.ForEach(orders, (order) =>
                    {
                        foreach (var exchangeOrder in order.ExchangeOrders.OrderBy(x => x.Sequence))
                        {
                            var binanceOrderInfo = new OrderInfo { Status = BinanceOrderStatus.NEW };
                            try
                            {
                                if (exchangeOrder.Status == OrderStatus.Open)
                                {
                                    binanceOrderInfo = b.Binance.NewMarketOrder(exchangeOrder.Symbol, exchangeOrder.Side, exchangeOrder.BaseQty);
                                    exchangeOrder.ExchangeOrderId = binanceOrderInfo.ClientOrderId;
                                    exchangeOrder.Status = OrderStatus.InProcess;
                                    exchangeOrder.StatusMsg = null;
                                }
                                else
                                {
                                    binanceOrderInfo = b.Binance.QueryOrder(exchangeOrder.Symbol, exchangeOrder.ExchangeOrderId);
                                }
                            }
                            catch (BinanceException bex)
                            {
                                if (exchangeOrder.Status == OrderStatus.Open) exchangeOrder.Status = OrderStatus.Cancelled;
                                exchangeOrder.StatusMsg = bex.Msg;
                            }

                            if (binanceOrderInfo.Status == BinanceOrderStatus.FILLED)
                            {
                                exchangeOrder.QuoteQty = binanceOrderInfo.CummulativeQuoteQty;
                                exchangeOrder.Status = OrderStatus.Completed;
                                exchangeOrder.StatusMsg = null;
                            }
                            else if (binanceOrderInfo.Status == (BinanceOrderStatus.CANCELED | BinanceOrderStatus.EXPIRED | BinanceOrderStatus.REJECTED))
                            {
                                exchangeOrder.Status = OrderStatus.Cancelled;
                                exchangeOrder.StatusMsg = binanceOrderInfo.Status.ToString() + " by Binance";
                            }

                            db.UpdateExchangeOrder(exchangeOrder);

                            if (binanceOrderInfo.Status != BinanceOrderStatus.FILLED) break;
                        }
                    });
                }

                db.UpdateBotStatus(b.User.Id, BotStatus.Stopped);
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error("Error: " + ex.Message, ex);
                    db.UpdateBotStatus(b.User.Id, BotStatus.Stopped, "Stopped due to error");
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
