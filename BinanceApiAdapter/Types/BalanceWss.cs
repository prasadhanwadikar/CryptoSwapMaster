using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class BalanceWss
    {
        [JsonProperty("a")]
        public string Asset { get; set; }

        [JsonProperty("f")]
        public decimal Free { get; set; }

        [JsonProperty("l")]
        public decimal Locked { get; set; }
    }
}
