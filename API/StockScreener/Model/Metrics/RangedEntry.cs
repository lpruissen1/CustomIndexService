using StockScreener.Core;

namespace StockScreener.Model.Metrics
{
    public class RangedEntry
    {
        private Range range;
        private TimeSpan span;

        public RangedEntry(Range range, TimeSpan span)
        {
            this.range = range;
            this.span = span;
        }

        public bool Valid(double value)
        {
            return range.WithinRange(value);
        }

        public TimeSpan GetTimeSpan()
        {
            return span;
        }
    }
}
