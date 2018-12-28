using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Types
{
    public class OrderFill
    {
        public double Price { get; set; }
        public double Qty { get; set; }
        public double Commission { get; set; }
        public string CommissionAsset { get; set; }
    }
}
