using System.Collections.Generic;
using BinanceApiAdapter.Enums;
using Newtonsoft.Json;

namespace BinanceApiAdapter.Types
{
    public class OrderInfo
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string OrigClientOrderId { get; set; }
        public string ClientOrderId { get; set; }
        public long TransactTime { get; set; }
        public double Price { get; set; }
        public double OrigQty { get; set; }
        public double ExecutedQty { get; set; }
        public double CummulativeQuoteQty { get; set; }
        [JsonProperty("status")]
        public OrderStatus OrderStatus { get; set; }
        public TimeInForce TimeInForce { get; set; }
        [JsonProperty("type")]
        public OrderType OrderType { get; set; }
        [JsonProperty("side")]
        public OrderSide OrderSide { get; set; }
        public List<OrderFill> Fills { get; set; }

        public OrderInfo()
        {
            Fills = new List<OrderFill>();
        }
    }
}