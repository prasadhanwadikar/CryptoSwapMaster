using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.BinanceApiAdapter.Enums
{
    public enum SymbolStatus
    {
        PRE_TRADING,
        TRADING,
        POST_TRADING,
        END_OF_DAY,
        HALT,
        AUCTION_MATCH,
        BREAK
    }
}
