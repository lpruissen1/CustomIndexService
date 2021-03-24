using StockScreener.Core;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model
{
    public class MarketCapMetric : IMetric
    {
        public MarketCapMetric(List<Range> ranges)
        {
            marketCaps = ranges;
        }

        private List<Range> marketCaps { get; init; }

        public void AddMarketCap(Range marketCapRange)
        {
            marketCaps.Add(marketCapRange);
        }

        public void Apply(ref SecuritiesList securitiesList)
        {
            securitiesList.RemoveAll(security => !marketCaps.Any(range => range.WithinRange(security.MarketCap)));
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            yield return Datapoint.MarketCap;
        }
    }
}
