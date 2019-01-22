using System.Collections.Generic;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class AccountInfo
    {
        public double MakerCommission { get; set; }
        public double TakerCommission { get; set; }
        public double BuyerCommission { get; set; }
        public double SellerCommission { get; set; }
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
