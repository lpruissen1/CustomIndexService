using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class PayoutRatioMetric : IMetric
    {
        public PayoutRatioMetric(List<Range> ranges)
        {
            payoutRatio = ranges;
        }

        private List<Range> payoutRatio { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !payoutRatio.Any(range => range.WithinRange(security.PayoutRatio)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.PayoutRatio;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.PayoutRatio };
        }
    }
}
