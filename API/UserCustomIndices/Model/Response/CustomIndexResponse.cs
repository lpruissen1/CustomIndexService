namespace UserCustomIndices.Model.Response
{
    public class CustomIndexResponse
    {
        public string Id { get; set; }
        public ComposedMarkets Markets { get; set; }
        public DividendYield DividendYield { get; set; }
        public Volitility Volitility { get; set; }
        public TrailingPerformance TrailingPerformance { get; set; }
        public RevenueGrowth RevenueGrowth { get; set; }
        public EarningsGrowth EarningsGrowth { get; set; }
        public Sectors SectorAndIndsutry { get; set; }
        public MarketCaps MarketCaps { get; set; }
        public string Test { get; set; }
    }
}
