using Database.Core;
using System.Collections.Generic;
using UserCustomIndices.Database.Model.User.CustomIndices;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public string IndexId { get; init; }
        public List<string> Markets { get; init; } = new List<string>();
        public Sector Sector { get; set; }
		public bool Active { get; set; }
        public List<TimedRangeRule> TimedRangeRule { get; init; } = new List<TimedRangeRule>();
        public List<RangedRule> RangedRule { get; init; } = new List<RangedRule>();

        //public List<DividendYield> DividendYields { get; init; } = new List<DividendYield>();
        //public List<CoefficientOfVariation> CoefficientOfVariation { get; init; } = new List<CoefficientOfVariation>();
        //public List<RevenueGrowthAnnualized> RevenueGrowthAnnualized { get; init; } = new List<RevenueGrowthAnnualized>();
        //public List<EPSGrowthAnnualized> EPSGrowthAnnualized { get; init; } = new List<EPSGrowthAnnualized>();
        //public List<AnnualizedTrailingPerformance> TrailingPerformanceAnnualized { get; init; } = new List<AnnualizedTrailingPerformance>();
        //public List<PriceToEarningsRatioTTM> PriceToEarningsRatioTTM { get; init; } = new List<PriceToEarningsRatioTTM>();
        //public List<PriceToSalesRatioTTM> PriceToSalesRatioTTM { get; init; } = new List<PriceToSalesRatioTTM>();
        //public List<Sector> SectorAndIndsutry { get; init; } = new List<Sector>();
        //public List<MarketCapitalization> MarketCaps { get; init; } = new List<MarketCapitalization>();
    }
}
