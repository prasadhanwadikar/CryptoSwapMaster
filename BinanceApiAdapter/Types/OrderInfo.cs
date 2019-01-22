﻿using System.Collections.Generic;
using CryptoSwapMaster.BinanceApiAdapter.Enums;
using Newtonsoft.Json;

namespace CryptoSwapMaster.BinanceApiAdapter.Types
{
    public class OrderInfo
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string OrigClientOrderId { get; set; }
        public string ClientOrderId { get; set; }
        public long TransactTime { get; set; }
        public double Price { get; set; }
        public double OrigQty { get; set; }
        public double ExecutedQty { get; set; }
        public double CummulativeQuoteQty { get; set; }
        public BinanceOrderStatus Status { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public BinanceOrderType Type { get; set; }
        public BinanceOrderSide Side { get; set; }
        public List<OrderFill> Fills { get; set; }

        public OrderInfo()
        {
            Fills = new List<OrderFill>();
        }
    }
}