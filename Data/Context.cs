using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoSwapMaster.Data.Entities;

namespace CryptoSwapMaster.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=CryptoSwapMaster")
        {
            Database.SetInitializer<Context>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(p => p.BaseQty).HasPrecision(18, 8);
            modelBuilder.Entity<Order>().Property(p => p.ExecutedBaseQty).HasPrecision(18, 8);
            modelBuilder.Entity<Order>().Property(p => p.ExpectedQuoteQty).HasPrecision(18, 8);
            modelBuilder.Entity<Order>().Property(p => p.ReceivedQuoteQty).HasPrecision(18, 8);
            modelBuilder.Entity<ExchangeOrder>().Property(p => p.BaseQty).HasPrecision(18, 8);
            modelBuilder.Entity<ExchangeOrder>().Property(p => p.QuoteQty).HasPrecision(18, 8);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Key> Keys { get; set; }

        public DbSet<QuoteAsset> QuoteAssets { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ExchangeOrder> ExchangeOrders { get; set; }
    }
}
