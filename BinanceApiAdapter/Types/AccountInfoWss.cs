using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class AccountInfoWss
    {
        [JsonProperty("e")]
        public string EventType { get; set; }

        [JsonProperty("E")]
        public long EventTime { get; set; }

        [JsonProperty("m")]
        public decimal MakerCommission { get; set; }

        [JsonProperty("t")]
        public decimal TakerCommission { get; set; }

        [JsonProperty("b")]
        public decimal BuyerCommission { get; set; }

        [JsonProperty("s")]
        public decimal SellerCommission { get; set; }

        [JsonProperty("T")]
        public bool CanTrade { get; set; }

        [JsonProperty("W")]
        public bool CanWithdraw { get; set; }

        [JsonProperty("D")]
        public bool CanDeposit { get; set; }

        [JsonProperty("u")]
        public long UpdateTime { get; set; }

        [JsonProperty("B")]
        public List<BalanceWss> Balances { get; set; }

        public AccountInfoWss()
        {
            Balances = new List<BalanceWss>();
        }
    }
}
