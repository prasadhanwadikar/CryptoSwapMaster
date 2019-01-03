using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

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
                context.SaveChanges();
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
                            BotStatus = BotStatus.Stopped
                        };
                        context.Users.Add(user);
                    }
                    else
                    {
                        user.Ip = ip;
                    }
                }
                else
                {
                    user.ApiKey = apiKey;
                    user.SecretKey = secretKey;
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
    }
}
