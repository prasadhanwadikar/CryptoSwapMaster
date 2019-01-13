﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BinanceApiAdapter;

namespace BotsManagerService
{
    public class Bot
    {
        public User User { get; set; }
        public Task Observer { get; set; }
        public Task Processor { get; set; }
        public CancellationTokenSource CTS { get; set; }
        public BinanceApiClient Binance { get; set; }
    }
}
