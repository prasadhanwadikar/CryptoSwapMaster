using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Enums
{
    public enum BinanceOrderStatus
    {
        NEW,
        PARTIALLY_FILLED,
        FILLED,
        CANCELED,
        PENDING_CANCEL, //(currently unused)
        REJECTED,
        EXPIRED
    }
}
