using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class Bid
    {
        public decimal Price { get; }
        public decimal Qty { get; }

        public Bid(decimal price, decimal qty)
        {
            Price = price;
            Qty = qty;
        }
    }
}
