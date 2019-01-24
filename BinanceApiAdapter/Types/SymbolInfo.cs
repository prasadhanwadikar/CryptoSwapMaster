using System.Collections.Generic;
using CryptoSwapMaster.BinanceApiAdapter.Enums;
using Newtonsoft.Json;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class SymbolInfo
    {
        public string Symbol { get; set; }
        public string Status { get; set; }
        public string BaseAsset { get; set; }
        public int BaseAssetPrecision { get; set; }
        public string QuoteAsset { get; set; }
        public int QuotePrecision { get; set; }
        public decimal Price { get; set; }
        public List<BinanceOrderType> OrderTypes { get; set; }
        public bool IcebergAllowed { get; set; }
        public List<Filter> Filters { get; set; }

        public SymbolInfo()
        {
            OrderTypes = new List<BinanceOrderType>();
            Filters = new List<Filter>();
        }
    }
}