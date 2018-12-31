using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Types
{
    public class BalanceWss
    {
        [JsonProperty("a")]
        public string Asset { get; set; }

        [JsonProperty("f")]
        public double Free { get; set; }

        [JsonProperty("l")]
        public double Locked { get; set; }
    }
}
