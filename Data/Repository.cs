using System;
using System.Collections.Generic;
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

        public User SaveUser(string ip, string apiKey, string secretKey, List<string> selectedAssets)
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
                            SecretKey = secretKey
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
                foreach (var asset in selectedAssets)
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
