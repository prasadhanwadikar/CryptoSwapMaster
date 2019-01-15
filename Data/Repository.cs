using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Data.Entities;
using Data.Enums;

namespace Data
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

        public void CancelOpenOrders(int userId, string baseAsset)
        {
            using (var context = new Context())
            {
                var orders = context.Orders.Where(x => x.UserId == userId && x.Status == OrderStatus.Open && x.BaseAsset == baseAsset);
                foreach (var order in orders)
                {
                    order.Status = OrderStatus.Cancelled;
                    order.StatusMsg = "Base asset free balance found insufficient for the specified groups of orders";
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
                dbExchangeOrder.QuoteQty = exchangeOrder.QuoteQty;
                dbExchangeOrder.Status = exchangeOrder.Status;
                dbExchangeOrder.StatusMsg = exchangeOrder.StatusMsg;
                dbExchangeOrder.LastModified = DateTime.Now;

                var dbNextExchangeOrder = order.ExchangeOrders.FirstOrDefault(x => x.Sequence == 2);

                if (dbExchangeOrder.Sequence == 2 || dbNextExchangeOrder == null)
                {
                    order.ReceivedQuoteQty = exchangeOrder.QuoteQty; //fine to be updated even when exchangeOrder.Status == InProcess
                }
                else
                {
                    if (exchangeOrder.Status == OrderStatus.Cancelled)
                    {
                        dbNextExchangeOrder.Status = exchangeOrder.Status;
                        dbNextExchangeOrder.StatusMsg = "First leg of order was failed or cancelled";
                        dbNextExchangeOrder.LastModified = DateTime.Now;
                    }
                    else if (exchangeOrder.Status == OrderStatus.Completed)
                    {
                        dbNextExchangeOrder.BaseQty = exchangeOrder.QuoteQty ?? 0.0;
                        dbNextExchangeOrder.LastModified = DateTime.Now;
                    }
                }

                order.Status = exchangeOrder.Status;
                order.StatusMsg = exchangeOrder.StatusMsg;
                order.LastModified = DateTime.Now;

                context.SaveChanges();
            }
        }
    }
}
