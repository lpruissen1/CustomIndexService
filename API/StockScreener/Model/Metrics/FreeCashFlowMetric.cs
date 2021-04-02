using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class FreeCashFlowMetric : IMetric
    {
        public FreeCashFlowMetric(List<Range> ranges)
        {
            freeCashFlow = ranges;
        }

        private List<Range> freeCashFlow { get; init; }

        public void AddFreeCashFlowRange(Range freeCashFlowRange)
        {
            freeCashFlow.Add(freeCashFlowRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !freeCashFlow.Any(range => range.WithinRange(security.FreeCashFlow)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.FreeCashFlow;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.FreeCashFlow };
        }
    }
}
