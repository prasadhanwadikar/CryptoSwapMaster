using System.Collections.Generic;

namespace BinanceApiAdapter.Types
{
    public class ExchangeInfo
    {
        public string TimeZone { get; set; }
        public string ServerTime { get; set; }
        public List<RateLimit> RateLimits { get; set; }
        public List<object> ExchangeFilters { get; set; }
        public List<SymbolInfo> Symbols { get; set; }

        public ExchangeInfo()
        {
            RateLimits = new List<RateLimit>();
            ExchangeFilters = new List<object>();
            Symbols = new List<SymbolInfo>();
        }
    }
}