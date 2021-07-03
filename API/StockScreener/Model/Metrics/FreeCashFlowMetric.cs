using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class FreeCashFlowMetric : RangedMetric
    {
        public FreeCashFlowMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.FreeCashFlow;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.FreeCashFlow };
        }

        public override double? GetValue(DerivedSecurity security)
        {
            return security.FreeCashFlow;
        }
    }
}
