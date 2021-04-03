using Core;
using StockScreener.Core;

namespace StockScreener.Model.Metrics
{
    public class RangeAndTimePeriod
    {
        private Range range;
        private TimePeriod span;

        public RangeAndTimePeriod(Range range, TimePeriod span)
        {
            this.range = range;
            this.span = span;
        }

        public RangeAndTimePeriod(Range range)
        {
            this.range = range;
        }

        public bool Valid(double value)
        {
            return range.WithinRange(value);
        }

        public TimePeriod GetTimePeriod()
        {
            return span;
        }
    }
}
