using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
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

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !priceToEarningsRatios.Any(range => range.WithinRange(security.PriceToEarningsRatioTTM)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
        }

        public IEnumerable<BaseDatapoint> GetDerivedDatapoints()
        {
            throw new System.NotImplementedException();
        }
    }
}
