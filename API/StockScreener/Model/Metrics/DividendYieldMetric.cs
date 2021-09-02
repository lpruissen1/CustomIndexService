using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class DividendYieldMetric : RangedMetric
    {
        public DividendYieldMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
            yield return BaseDatapoint.DividendsPerShare;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Rule = RuleType.DividendYield };
		}

		public override TimePeriod? GetPriceTimePeriod()
		{
			return TimePeriod.Year;
		}

		public override double? GetValue(DerivedSecurity security)
        {
            return security.DividendYield;
        }
    }
}
