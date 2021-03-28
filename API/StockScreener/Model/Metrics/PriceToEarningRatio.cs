using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class PriceToEarningRatio : IMetric
    {
        private List<RangedEntry> entries;

        public PriceToEarningRatio(List<RangedEntry> ranges)
        {
            entries = ranges;
        }

        public void Add(RangedEntry priceToEarningsRatioRange)
        {
            entries.Add(priceToEarningsRatioRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !entries.Any(range => range.Valid(security.PriceToEarningsRatio[range.GetTimeSpan()])));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
            yield return BaseDatapoint.Earnings;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            // todo: yield return for all time ranges
            throw new System.NotImplementedException();
        }
    }
}
