﻿using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
	public class CurrentRatioMetric : RangedMetric
    {
        public CurrentRatioMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.CurrentRatio;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
		{
			//wrong
			yield return new DerivedDatapointConstructionData { Rule = RuleType.SectorIndustry };
		}

		public override double? GetValue(DerivedSecurity security)
        {
            return security.CurrentRatio;
        }
    }
}
