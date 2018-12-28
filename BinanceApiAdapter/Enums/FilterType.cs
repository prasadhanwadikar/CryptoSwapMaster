using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Enums
{
    public enum FilterType
    {
        PRICE_FILTER,
        PERCENT_PRICE,
        LOT_SIZE,
        MIN_NOTIONAL,
        MAX_NUM_ORDERS,
        MAX_NUM_ALGO_ORDERS,
        ICEBERG_PARTS,
        EXCHANGE_MAX_NUM_ORDERS,
        EXCHANGE_MAX_NUM_ALGO_ORDERS
    }
}
