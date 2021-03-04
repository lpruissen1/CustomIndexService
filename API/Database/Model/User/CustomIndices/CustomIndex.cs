using System;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public ComposedMarkets Markets { get; init; } = new ComposedMarkets();
        public DividendYield DividendYield { get; init; } 
        public Volitility Volitility { get; init; }
        public TrailingPerformance TrailingPerformance { get; init; }
        public RevenueGrowth RevenueGrowth { get; init; }
        public EarningsGrowth EarningsGrowth { get; init; }
        public Sectors SectorAndIndsutry { get; init; } = new Sectors();
        public MarketCaps MarketCaps { get; init; }
        public string Test { get; init; }

        public override string GetPrimaryKey()
        {
            return Id;
        }
    }
}