using Core;
using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public class DerivedSecurity : Security
    {
        public Dictionary<TimePeriod, double> RevenueGrowthAnnualized;
        public Dictionary<TimePeriod, double> EPSGrowthAnnualized;
        public double PriceToEarningsRatioTTM;
        public double PriceToSalesRatioTTM;
        public double PriceToBookValue;
        public double DividendYield;
        public Dictionary<TimePeriod, double> TrailingPerformance;
    }

}
