using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public class Context : DbContext
    {
        public Context() : base("name=CryptoSwapMaster")
        {
            Database.SetInitializer<Context>(null);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<QuoteAsset> QuoteAssets { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ExchangeOrder> ExchangeOrders { get; set; }
    }
}
