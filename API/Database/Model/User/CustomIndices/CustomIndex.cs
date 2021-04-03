using Database.Core;
using System.Collections.Generic;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public ComposedMarkets Markets { get; init; } = new ComposedMarkets();
        public DividendYield DividendYield { get; init; } 
        public Volitility Volitility { get; init; }
        public TrailingPerformance TrailingPerformance { get; init; }
        public List<RevenueGrowth> RevenueGrowths { get; init; } = new List<RevenueGrowth>();
        public EarningsGrowth EarningsGrowth { get; init; }
        public List<PriceToEarningRatioTTM> PriceToEarningRevenue { get; init; } = new List<PriceToEarningRatioTTM>();
        public Sectors SectorAndIndsutry { get; init; } = new Sectors();
        public MarketCaps MarketCaps { get; init; } = new MarketCaps();
        public string Test { get; init; }
    }
}