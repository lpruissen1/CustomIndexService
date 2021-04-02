using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
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

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !marketCaps.Any(range => range.WithinRange(security.MarketCap)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.MarketCap;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.MarketCap };
        }
    }
}
