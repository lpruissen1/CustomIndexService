using Core;
using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public class DerivedSecurity : Security
    {
        public Dictionary<TimePeriod, double> RevenueGrowth;
        public Dictionary<TimePeriod, double> EarningsGrowth;
        public double PriceToEarningsRatioTTM;
        public Dictionary<TimePeriod, double> TrailingPerformance;
    }

}
