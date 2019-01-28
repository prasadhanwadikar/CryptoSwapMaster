using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class UserStreamPayloadType
    {
        [JsonProperty("e")]
        public string EventType { get; set; }
    }
}
