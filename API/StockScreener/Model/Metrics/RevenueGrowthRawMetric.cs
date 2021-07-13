﻿using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class RevenueGrowthRawMetric : RangeAndTimePeriodMetric
    {
        public RevenueGrowthRawMetric(List<RangeAndTimePeriod> rangesAndTimeSpans) : base(rangesAndTimeSpans) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Revenue;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach (var entry in rangedDatapoint.GroupBy(x => x.GetTimePeriod()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.RevenueGrowthRaw, Time = entry.GetTimePeriod() };
            }
		}

		public override Dictionary<TimePeriod, double?> GetValue(DerivedSecurity security)
        {
            return security.RevenueGrowthRaw;
        }
    }
}
