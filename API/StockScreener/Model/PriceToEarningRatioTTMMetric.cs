using StockScreener.Core;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model
{
    public class PriceToEarningRatioTTMMetric : IMetric
    {
        public PriceToEarningRatioTTMMetric(List<Range> ranges)
        {
            priceToEarningsRatios = ranges;
        }

        private List<Range> priceToEarningsRatios { get; init; }

        public void AddMarketCap(Range marketCapRange)
        {
            priceToEarningsRatios.Add(marketCapRange);
        }

        public void Apply(ref SecuritiesList securitiesList)
        {
            securitiesList.RemoveAll(security => !priceToEarningsRatios.Any(range => range.WithinRange(security.PriceToEarningsRatioTTM)));
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            yield return Datapoint.EarningsPerShare;
            yield return Datapoint.CurrentPrice;
        }
    }
}
