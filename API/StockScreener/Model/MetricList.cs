using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model
{
    public class MetricList : List<IMetric>
    {
        internal MarketMetric MarketMetric => this.OfType<MarketMetric>().FirstOrDefault();
    }
}
