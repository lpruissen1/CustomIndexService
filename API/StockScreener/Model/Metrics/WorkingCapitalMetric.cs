using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class WorkingCapitalMetric : RangedMetric
    {
        public WorkingCapitalMetric(List<Range> ranges) : base(ranges)
        {
        }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.WorkingCapital;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.WorkingCapital };
		}

		public override double? GetValue(DerivedSecurity security)
        {
            return security.WorkingCapital;
        }
    }
}
