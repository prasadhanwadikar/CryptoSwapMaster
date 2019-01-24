using CryptoSwapMaster.BinanceApiAdapter.Enums;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class Filter
    {
        public FilterType FilterType { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal TickSize { get; set; }
        public decimal MinQty { get; set; }
        public decimal MaxQty { get; set; }
        public decimal StepSize { get; set; }
        public decimal MinNotional { get; set; }
        public int Limit { get; set; }
        public int MaxNumAlgoOrders { get; set; }
        public decimal MultiplierUp { get; set; }
        public decimal MultiplierDown { get; set; }
        public decimal AvgPriceMins { get; set; }
    }
}