using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class CurrentRatioMetric : IMetric
    {
        public CurrentRatioMetric(List<Range> ranges)
        {
            currentRatio = ranges;
        }

        private List<Range> currentRatio { get; init; }

        public void AddCurrentRatio(Range currentRatioRange)
        {
            currentRatio.Add(currentRatioRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !currentRatio.Any(range => range.WithinRange(security.CurrentRatio)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.CurrentRatio;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.CurrentRatio };
        }
    }
}
