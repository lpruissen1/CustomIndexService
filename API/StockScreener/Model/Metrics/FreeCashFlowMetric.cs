using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

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
			// wrong
            yield return new DerivedDatapointConstructionData { Rule = RuleType.AnnualizedTrailingPerformance };
        }

		public override double? GetValue(DerivedSecurity security)
        {
            return security.FreeCashFlow;
        }
    }
}
