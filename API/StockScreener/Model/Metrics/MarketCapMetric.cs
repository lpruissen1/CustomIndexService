using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
	public class MarketCapMetric : RangedMetric
    {
        public MarketCapMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.MarketCap;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Rule = RuleType.MarketCap };
        }

		public override double? GetValue(DerivedSecurity security)
        {
            return security.MarketCap;
        }
    }
}
