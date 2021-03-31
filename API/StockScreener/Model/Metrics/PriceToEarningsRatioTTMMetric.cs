using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class PriceToEarningsRatioTTMMetric : IMetric
    {
        private List<RangedEntry> entries;

        public PriceToEarningsRatioTTMMetric(List<RangedEntry> ranges)
        {
            entries = ranges;
        }

        public void Add(RangedEntry priceToEarningsRatioRange)
        {
            entries.Add(priceToEarningsRatioRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !entries.Any(range => range.Valid(security.PriceToEarningsRatioTTM)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
            yield return BaseDatapoint.QuarterlyEarningsPerShare;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.PriceToEarningsRatioTTM };
        }
    }
}
