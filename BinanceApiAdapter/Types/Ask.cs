using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class Ask
    {
        public decimal Price { get; }
        public decimal Qty { get; }

        public Ask(decimal price, decimal qty)
        {
            Price = price;
            Qty = qty;
        }
    }
}
