using CryptoSwapMaster.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoSwapMaster.BinanceApiAdapter;

namespace CryptoSwapMaster.Service
{
    public class Bot
    {
        public int UserId { get; set; }
        public Task Observer { get; set; }
        public Task Processor { get; set; }
        public CancellationTokenSource CTS { get; set; }
        public BinanceApiClient Exchange { get; set; }
    }
}
