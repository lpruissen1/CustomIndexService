﻿using StockScreener.Core;

namespace StockScreener.Model.Metrics
{
    public class RangeAndTimeSpan
    {
        private Range range;
        private TimeSpan span;

        public RangeAndTimeSpan(Range range, TimeSpan span)
        {
            this.range = range;
            this.span = span;
        }

        public RangeAndTimeSpan(Range range)
        {
            this.range = range;
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