namespace StockScreener.Core
{
    public class Range
    {
        public Range(double upper, double lower)
        {
            LowerBound = lower;
            UpperBound = upper;
        }

        private double LowerBound;
        private double UpperBound;

        public bool WithinRange(double? value)
        {
			if (value is null)
				return false;

            return LowerBound <= value.Value && UpperBound >= value.Value;
        }
    }
}
