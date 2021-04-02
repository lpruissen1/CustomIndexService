using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class GrossMarginMetric : IMetric
    {
        public GrossMarginMetric(List<Range> ranges)
        {
            grossMargin = ranges;
        }

        private List<Range> grossMargin { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !grossMargin.Any(range => range.WithinRange(security.GrossMargin)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.GrossMargin;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.GrossMargin };
        }
    }
}
