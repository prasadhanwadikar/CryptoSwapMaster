using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotsManagerService
{
    public class Bot
    {
        public User User { get; set; }
        public Task Task { get; set; }
        public CancellationTokenSource CTS { get; set; }
    }
}
