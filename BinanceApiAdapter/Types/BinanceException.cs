using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApiAdapter.Types
{
    public class BinanceException : Exception
    {
        public int Code { get; }
        public string Msg { get; }

        public BinanceException(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }
    }
}
