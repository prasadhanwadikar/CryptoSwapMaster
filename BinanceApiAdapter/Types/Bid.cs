using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class Bid
    {
        public double Price { get; }
        public double Qty { get; }

        public Bid(double price, double qty)
        {
            Price = price;
            Qty = qty;
        }
    }
}
