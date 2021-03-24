namespace StockScreener.Core
{
    public static class GrowthRateCalculator
    {
        public static double CalculateGrowthRate(double present, double past)
        {
            return (present - past) / past * 100;
        }
    }

}
