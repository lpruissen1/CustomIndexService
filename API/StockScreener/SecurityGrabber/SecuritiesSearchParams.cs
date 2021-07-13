using Core;
using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.SecurityGrabber
{
    public struct SecuritiesSearchParams
    {
        public IEnumerable<string> Markets { get; set; }
        public IEnumerable<BaseDatapoint> Datapoints {get; set; } 
        public TimePeriod? PriceTimePeriod {get; set; } 
    }
}
