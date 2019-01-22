using CryptoSwapMaster.BinanceApiAdapter.Enums;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class RateLimit
    {
        public RateLimitType RateLimitType { get; set; }
        public RateLimitInterval Interval { get; set; }
        public int Limit { get; set; }
    }
}