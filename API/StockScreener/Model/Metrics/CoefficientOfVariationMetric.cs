﻿using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class CoefficientOfVariationMetric : RangeAndTimePeriodMetric
    {
        public CoefficientOfVariationMetric(List<RangeAndTimePeriod> rangesAndTimeSpans) : base(rangesAndTimeSpans) {}

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach(var entry in rangedDatapoint.GroupBy(x => x.GetTimePeriod()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { Rule = RuleType.CoefficientOfVariation, Time = entry.GetTimePeriod() };
            }
        }

		public override TimePeriod? GetPriceTimePeriod()
		{
			return rangedDatapoint.Max(x => x.GetTimePeriod());
		}

		public override Dictionary<TimePeriod, double?> GetValue(DerivedSecurity security)
        {
            return security.CoefficientOfVariation;
        }
    }
}
