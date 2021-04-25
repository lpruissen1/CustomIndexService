namespace UserCustomIndices.Core
{
    public enum RuleType
    {
        SectorIndustry,
        DividendYield,
        CoefficientOfVariation,
        RevenueGrowthAnnualized,
        EPSGrowthAnnualized,
        AnnualizedTraillingPerformance,
        PriceToEarningsRatioTTM,
        PriceToSalesRatioTTM,
        MarketCap
    }

    public enum TimedRangeRuleType
    {
        CoefficientOfVariation,
        RevenueGrowthAnnualized,
        EPSGrowthAnnualized,
        AnnualizedTraillingPerformance
    }

    public enum RangedRuleType
    {
        DividendYield,
        PriceToEarningsRatioTTM,
        PriceToSalesRatioTTM,
        MarketCap
    }
}
