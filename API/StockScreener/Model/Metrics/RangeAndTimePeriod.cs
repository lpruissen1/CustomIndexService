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

        public bool Valid(double? value)
        {
			if (value is null)
				return false;

            return range.WithinRange(value.Value);
        }

        public TimePeriod GetTimePeriod()
        {
            return span;
        }
    }
}
