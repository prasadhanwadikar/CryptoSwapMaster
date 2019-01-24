using System.Collections.Generic;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class AccountInfo
    {
        public decimal MakerCommission { get; set; }
        public decimal TakerCommission { get; set; }
        public decimal BuyerCommission { get; set; }
        public decimal SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public long UpdateTime { get; set; }
        public List<Balance> Balances { get; set; }

        public AccountInfo()
        {
            Balances = new List<Balance>();
        }
    }
}
