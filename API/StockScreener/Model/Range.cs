namespace StockScreener.Model
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

        public bool WithinRange(double value)
        {
            return LowerBound <= value && UpperBound >= value;
        }
    }
}
