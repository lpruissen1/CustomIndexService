using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public class DerivedSecurity : Security
    {
        public Dictionary<TimeSpan, double> RevenueGrowth;
        public Dictionary<TimeSpan, double> EPSGrowthAnnualized;
        public double PriceToEarningsRatioTTM;
        public double PriceToSalesRatioTTM;
        public double PriceToBookValue;
        public double DividendYield;
        public Dictionary<TimeSpan, double> TrailingPerformance;
    }

}
