using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.SecurityGrabber
{
    public struct SecuritiesSearchParams
    {
        public IEnumerable<string> Indices { get; set; }
        public IEnumerable<BaseDatapoint> Datapoints {get; set; } 
    }
}
