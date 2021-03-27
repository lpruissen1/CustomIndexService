using StockScreener.Core;

namespace StockScreener.Model
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

        public bool Valid(Security security)
        {
            return range.WithinRange(security.RevenueGrowth[span]);
        }
    }
}
