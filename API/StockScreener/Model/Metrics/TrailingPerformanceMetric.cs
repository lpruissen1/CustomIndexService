﻿using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class TrailingPerformanceMetric : RangeAndTimPeriodMetric
    {
        public TrailingPerformanceMetric(List<RangeAndTimePeriod> rangesAndTimeSpans) : base(rangesAndTimeSpans) {}

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach(var entry in rangedDatapoint.GroupBy(x => x.GetTimePeriod()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.TrailingPerformance, Time = entry.GetTimePeriod() };
            }
        }

        public override Dictionary<TimePeriod, double> GetValue(DerivedSecurity security)
        {
            return security.TrailingPerformance;
        }
    }
}