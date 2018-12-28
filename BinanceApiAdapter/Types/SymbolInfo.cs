using System.Collections.Generic;
using BinanceApiAdapter.Enums;
using Newtonsoft.Json;

namespace BinanceApiAdapter.Types
{
    public class SymbolInfo
    {
        public string Symbol { get; set; }
        public string Status { get; set; }
        public string BaseAsset { get; set; }
        public int BaseAssetPrecision { get; set; }
        public string QuoteAsset { get; set; }
        public int QuotePrecision { get; set; }
        public double Price { get; set; }
        public List<OrderType> OrderTypes { get; set; }
        public bool IcebergAllowed { get; set; }
        public List<Filter> Filters { get; set; }

        public SymbolInfo()
        {
            OrderTypes = new List<OrderType>();
            Filters = new List<Filter>();
        }
    }
}