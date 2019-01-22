using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class Balance
    {
        public string Asset { get; set; }
        public double Free { get; set; }
        public double Locked { get; set; }
    }
}
