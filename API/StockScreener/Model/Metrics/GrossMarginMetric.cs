using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class GrossMarginMetric : RangedMetric
    {
        public GrossMarginMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.GrossMargin;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.GrossMargin };
        }

		public override TimePeriod? GetPriceTimePeriod()
		{
			return null;
		}

		public override double? GetValue(DerivedSecurity security)
        {
            return security.GrossMargin;
        }
    }
}
