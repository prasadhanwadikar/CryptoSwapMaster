using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class OrderFill
    {
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Commission { get; set; }
        public string CommissionAsset { get; set; }
    }
}
