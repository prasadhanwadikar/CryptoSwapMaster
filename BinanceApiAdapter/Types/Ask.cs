using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Types
{
    public class Ask
    {
        public double Price { get; }
        public double Qty { get; }

        public Ask(double price, double qty)
        {
            Price = price;
            Qty = qty;
        }
    }
}
