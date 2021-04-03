using System;

namespace StockScreener.Core
{
    public static class GrowthRateCalculator
    {
        public static double CalculateGrowthRate(double present, double past)
        {
            return (present - past) / past * 100;
        }

        public static double CalculateAnnualizedGrowthRate(double present, double past, double timeSpan)
        {
            double yearUnixTime = 31_557_600;

            double valueRatio = present / past;
            double exponent = yearUnixTime / timeSpan;

            return (Math.Pow(valueRatio, exponent) - 1) * 100;
        }
    }
}
