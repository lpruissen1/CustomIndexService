using Database.Core;
using System.Collections.Generic;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public List<string> Markets { get; init; } = new List<string>();
        public List<DividendYield> DividendYields { get; init; } = new List<DividendYield>();
        public Volitility Volitility { get; init; }
        public List<AnnualizedTrailingPerformance> TrailingPerformance { get; init; } = new List<AnnualizedTrailingPerformance>();
        public List<RevenueGrowth> RevenueGrowths { get; init; } = new List<RevenueGrowth>();
        public EarningsGrowth EarningsGrowth { get; init; }
        public List<PriceToEarningsRatioTTM> PriceToEarningsRatioTTM { get; init; } = new List<PriceToEarningsRatioTTM>();
        public List<Sector> SectorAndIndsutry { get; init; } = new List<Sector>();
        public List<MarketCapitalzation> MarketCaps { get; init; } = new List<MarketCapitalzation>();
        public List<PayoutRatios> PayoutRatio { get; init; } = new List<PayoutRatios>();
        public List<ProfitMargins> ProfitMargin { get; init; } = new List<ProfitMargins>();
        public List<CurrentRatios> CurrentRatio { get; init; } = new List<CurrentRatios>();
        public List<GrossMargins> GrossMargin { get; set; } = new List<GrossMargins>();
        public List<WorkingCapitals> WorkingCapital { get; set; } = new List<WorkingCapitals>();
        public List<FreeCashFlows> FreeCashFlow { get; set; } = new List<FreeCashFlows>();
        public List<DebtToEquityRatios> DebtToEquityRatio { get; set; } = new List<DebtToEquityRatios>();
        public string Test { get; init; }
    }
}