using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using CryptoSwapMaster.Data.Entities;
using CryptoSwapMaster.Data.Enums;

namespace CryptoSwapMaster.Data
{
    public class Repository
    {
        public List<QuoteAsset> GetQuoteAssets(int userId)
        {
            using (var context = new Context())
            {
                return context.QuoteAssets.Where(x => x.UserId == userId).ToList();
            }
        }

        public User GetUser(string ip)
        {
            using (var context = new Context())
            {
                return context.Users.FirstOrDefault(x => x.Ip == ip);
            }
        }

        public User SaveUser(string ip, string apiKey, string secretKey, List<string> quoteAssets)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Ip == ip);
                if (user == null)
                {
                    user = context.Users.FirstOrDefault(x => x.ApiKey == apiKey && x.SecretKey == secretKey);
                    if (user == null)
                    {
                        user = new User()
                        {
                            Ip = ip,
                            ApiKey = apiKey,
                            SecretKey = secretKey,
                            BotStatus = BotStatus.Stopped,
                            Created = DateTime.Now
                        };
                        context.Users.Add(user);
                    }
                    else
                    {
                        user.Ip = ip;
                        user.LastModified = DateTime.Now;
                    }
                }
                else
                {
                    user.ApiKey = apiKey;
                    user.SecretKey = secretKey;
                    user.LastModified = DateTime.Now;
                }

                context.SaveChanges();

                var existingSelectedAssets = context.QuoteAssets.Where(x => x.UserId == user.Id);
                if (existingSelectedAssets.Any()) context.QuoteAssets.RemoveRange(existingSelectedAssets);
                foreach (var asset in quoteAssets)
                {
                    context.QuoteAssets.Add(new QuoteAsset()
                                                    {
                                                        UserId = user.Id,
                                                        Asset = asset
                                                    });
                }

                context.SaveChanges();

                return user;
            }
        }

        public List<User> GetUsers()
        {
            using (var context = new Context())
            {
                return context.Users.ToList();
            }
        }

        public void UpdateBotStatus(int userId, BotStatus botStatus, string message = null)
        {
            using (var context = new Context())
            {
                var user = context.Users.Find(userId);
                user.BotStatus = botStatus;
                user.BotStatusMsg = message;
                user.LastModified = DateTime.Now;
                context.SaveChanges();
            }
        }

        public List<Order> GetOrders(int userId, OrderStatus orderStatus)
        {
            using (var context = new Context())
            {
                return context.Orders.Include(x => x.ExchangeOrders).Where(x => x.UserId == userId && x.Status == orderStatus).ToList();
            }
        }

        public List<Order> GetOrders(int userId)
        {
            using (var context = new Context())
            {
                return context.Orders.Where(x => x.UserId == userId).ToList();
            }
        }

        public void AddOrder(int userId, string baseAsset, int pool, int group, string type, decimal baseQty, string quoteAsset, decimal quoteQty)
        {
            using (var context = new Context())
            {
                if (pool == 0)
                {
                    var baseAssetOrders = context.Orders.Where(x => x.UserId == userId && x.BaseAsset == baseAsset);
                    if (baseAssetOrders.Any()) pool = baseAssetOrders.Max(x => x.Pool) + 1;
                    else pool = 1;
                }

                if (group == 0)
                {
                    var poolOrders = context.Orders.Where(x => x.UserId == userId && x.BaseAsset == baseAsset && x.Pool == pool);
                    if (poolOrders.Any()) group = poolOrders.Max(x => x.Group) + 1;
                    else group = 1;
                }

                var order = new Order
                {
                    UserId = userId,
                    BaseAsset = baseAsset,
                    Pool = pool,
                    Group = group,
                    Type = type,
                    BaseQty = baseQty,
                    QuoteAsset = quoteAsset,
                    ExpectedQuoteQty = quoteQty,
                    Status = OrderStatus.Open,
                    Created = DateTime.Now
                };

                var anyPoolOrderInProcess = context.Orders.Any(x => x.UserId == userId
                                                                    && x.BaseAsset == baseAsset
                                                                    && x.Pool == pool
                                                                    && x.Status == (OrderStatus.InProcess | OrderStatus.Completed));
                if (anyPoolOrderInProcess) throw new Exception("Failed to add as other order from same pool is in process");

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public void CancelOrder(int orderId, string msg)
        {
            using (var context = new Context())
            {
                var order = context.Orders.Include(x => x.ExchangeOrders).FirstOrDefault(x => x.Id == orderId);
                if (order.ExchangeOrders.Any()) throw new Exception("Order already in process and can't be cancelled");
                order.Status = OrderStatus.Cancelled;
                order.StatusMsg = msg;
                order.LastModified = DateTime.Now;
                context.SaveChanges();
            }
        }

        public void CancelOpenOrders(int userId, string baseAsset)
        {
            using (var context = new Context())
            {
                var orders = context.Orders.Where(x => x.UserId == userId && x.Status == OrderStatus.Open && x.BaseAsset == baseAsset);
                foreach (var order in orders)
                {
                    order.Status = OrderStatus.Cancelled;
                    order.StatusMsg = "Base asset free balance found to be insufficient";
                    order.LastModified = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        public void MarkGroupOrdersInProcess(int userId, string baseAsset, int pool, int group)
        {
            using (var context = new Context())
            {
                var poolOrders = context.Orders.Where(x => x.UserId == userId && x.Status == OrderStatus.Open && x.BaseAsset == baseAsset && x.Pool == pool);
                foreach (var order in poolOrders)
                {
                    if (order.Group == group)
                    {
                        order.Status = OrderStatus.InProcess;
                    }
                    else
                    {
                        order.Status = OrderStatus.Cancelled;
                        order.StatusMsg = "Other specified option of order(s) got matched";
                    }

                    order.LastModified = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        public List<ExchangeOrder> GetExchangeOrders(int orderId)
        {
            using (var context = new Context())
            {
                return context.ExchangeOrders.Where(x => 
                    x.OrderId == orderId && x.Status == (OrderStatus.Open | OrderStatus.InProcess)).OrderBy(x => x.Sequence)
                    .ToList();
            }
        }

        public void SaveExchangeOrders(List<ExchangeOrder> exchangeOrders)
        {
            using (var context = new Context())
            {
                foreach (var exchangeOrder in exchangeOrders)
                {
                    context.ExchangeOrders.Add(exchangeOrder);
                }

                context.SaveChanges();
            }
        }

        public void UpdateExchangeOrder(ExchangeOrder exchangeOrder)
        {
            using (var context = new Context())
            {
                var order = context.Orders.Include(x => x.ExchangeOrders).FirstOrDefault(x => x.Id == exchangeOrder.OrderId);
                var dbExchangeOrder = order.ExchangeOrders.First(x => x.Sequence == exchangeOrder.Sequence);
                
                dbExchangeOrder.ExchangeOrderId = exchangeOrder.ExchangeOrderId;
                dbExchangeOrder.BaseQty = exchangeOrder.BaseQty;
                dbExchangeOrder.QuoteQty = exchangeOrder.QuoteQty;
                dbExchangeOrder.Status = exchangeOrder.Status;
                dbExchangeOrder.StatusMsg = exchangeOrder.StatusMsg;
                dbExchangeOrder.LastModified = DateTime.Now;

                if (exchangeOrder.Status != OrderStatus.InProcess)
                {
                    order.LastModified = DateTime.Now;

                    if (exchangeOrder.Sequence == 1 && exchangeOrder.Status == OrderStatus.Completed)
                    {
                        if (exchangeOrder.Side == "BUY")
                            order.ExecutedBaseQty = exchangeOrder.QuoteQty;
                        else
                            order.ExecutedBaseQty = exchangeOrder.BaseQty;
                    }

                    var qty = 0M;
                    if (exchangeOrder.Side == "BUY")
                        qty = exchangeOrder.BaseQty;
                    else
                        qty = exchangeOrder.QuoteQty;

                    if (exchangeOrder.Sequence == order.ExchangeOrders.Count) //if last order for a group
                    {
                        order.Status = exchangeOrder.Status;
                        if (exchangeOrder.Status == OrderStatus.Completed)
                        {
                            order.ReceivedQuoteQty = qty;
                            order.StatusMsg = null;
                        }
                        else
                        {
                            order.StatusMsg = (order.StatusMsg ?? "") + (exchangeOrder.StatusMsg ?? "");
                        }
                    }
                    else
                    {
                        var dbNextExchangeOrder = order.ExchangeOrders.FirstOrDefault(x => x.Sequence == 2);
                        if (exchangeOrder.Status == OrderStatus.Cancelled)
                        {
                            dbNextExchangeOrder.Status = OrderStatus.Cancelled;
                            dbNextExchangeOrder.StatusMsg = "First leg of order was failed or cancelled";
                            dbNextExchangeOrder.LastModified = DateTime.Now;

                            order.Status = OrderStatus.Cancelled;
                            order.StatusMsg = exchangeOrder.StatusMsg;
                        }
                        else if (exchangeOrder.Status == OrderStatus.Completed)
                        {
                            if (dbNextExchangeOrder.Side == "BUY")
                                dbNextExchangeOrder.QuoteQty = qty;
                            else
                                dbNextExchangeOrder.BaseQty = qty;

                            dbNextExchangeOrder.LastModified = DateTime.Now;

                            order.StatusMsg = "First leg converted " + order.BaseAsset + " " + order.ExecutedBaseQty + " to BTC " + qty.ToString() + ". ";
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
