using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class SymbolOrdersInfo
    {
        public long LastUpdateId { get; set; }
        public List<object[]> Bids { get; set; }
        public List<object[]> Asks { get; set; }

        public List<Bid> BidOrders
        {
            get
            {
                var bidOrders = new List<Bid>();
                foreach (var arr in Bids)
                {
                    var bid = new Bid(Convert.ToDecimal(arr[0]), Convert.ToDecimal(arr[1]));
                    bidOrders.Add(bid);
                }
                return bidOrders;
            }
        }
        public List<Ask> AskOrders
        {
            get
            {
                var askOrders = new List<Ask>();
                foreach (var arr in Asks)
                {
                    var ask = new Ask(Convert.ToDecimal(arr[0]), Convert.ToDecimal(arr[1]));
                    askOrders.Add(ask);
                }
                return askOrders;
            }
        }

        public SymbolOrdersInfo()
        {
            Bids = new List<object[]>();
            Asks = new List<object[]>();
        }
    }
}
