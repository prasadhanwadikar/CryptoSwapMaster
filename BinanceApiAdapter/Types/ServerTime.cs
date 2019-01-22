using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class ServerTime
    {
        [JsonProperty("serverTime")]
        public long Time { get; set; }
    }
}
