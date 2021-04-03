using Database.Core;
using System.Collections.Generic;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public ComposedMarkets Markets { get; init; } = new ComposedMarkets();
        public List<DividendYield> DividendYield { get; init; } = new List<DividendYield>();
        public Volitility Volitility { get; init; }
        public TrailingPerformance TrailingPerformance { get; init; }
        public List<RevenueGrowth> RevenueGrowths { get; init; } = new List<RevenueGrowth>();
        public EarningsGrowth EarningsGrowth { get; init; }
        public List<PriceToEarningsRatioTTM> PriceToEarningsRatioTTM { get; init; } = new List<PriceToEarningsRatioTTM>();
        public List<PriceToSalesRatioTTM> PriceToSalesRatioTTM { get; init; } = new List<PriceToSalesRatioTTM>();
        public Sectors SectorAndIndsutry { get; init; } = new Sectors();
        public MarketCaps MarketCaps { get; init; } = new MarketCaps();
        public List<PayoutRatios> PayoutRatio { get; init; } = new List<PayoutRatios>();
        public List<ProfitMargins> ProfitMargin { get; init; } = new List<ProfitMargins>();
        public List<PriceToBookValue> PriceToBookValue { get; init; } = new List<PriceToBookValue>();
        public List<CurrentRatios> CurrentRatio { get; init; } = new List<CurrentRatios>();
        public List<GrossMargins> GrossMargin { get; set; } = new List<GrossMargins>();
        public List<WorkingCapitals> WorkingCapital { get; set; } = new List<WorkingCapitals>();
        public List<FreeCashFlows> FreeCashFlow { get; set; } = new List<FreeCashFlows>();
        public List<DebtToEquityRatios> DebtToEquityRatio { get; set; } = new List<DebtToEquityRatios>();
        public string Test { get; init; }
    }
}