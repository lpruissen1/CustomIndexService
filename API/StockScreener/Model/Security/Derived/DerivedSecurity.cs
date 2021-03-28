﻿using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public class DerivedSecurity : Security
    {
        public Dictionary<TimeSpan, double> RevenueGrowth;
        public Dictionary<TimeSpan, double> EarningsGrowth;
        public Dictionary<TimeSpan, double> PriceToEarningsRatio;
        public Dictionary<TimeSpan, double> TrailingPerformance;
    }

}
