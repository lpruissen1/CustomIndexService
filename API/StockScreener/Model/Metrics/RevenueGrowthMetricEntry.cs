using StockScreener.Core;
using StockScreener.Model.BaseSecurity;

namespace StockScreener.Model.Metrics
{
    public class RevenueGrowthMetricEntry
    {
        private Range range;
        private TimeSpan span;

        public RevenueGrowthMetricEntry(Range range, TimeSpan span)
        {
            this.range = range;
            this.span = span;
        }

        public bool Valid(DerivedSecurity security)
        {
            return range.WithinRange(security.RevenueGrowth[span]);
        }
    }
}
