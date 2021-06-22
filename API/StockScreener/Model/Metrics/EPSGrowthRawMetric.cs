using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class EPSGrowthRawMetric : RangeAndTimePeriodMetric
    {
        public EPSGrowthRawMetric(List<RangeAndTimePeriod> rangesAndTimeSpans) : base(rangesAndTimeSpans) {}

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.QuarterlyEarningsPerShare;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach(var entry in rangedDatapoint.GroupBy(x => x.GetTimePeriod()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.EPSGrowthRaw, Time = entry.GetTimePeriod() };
            }
        }

        public override Dictionary<TimePeriod, double?> GetValue(DerivedSecurity security)
        {
            return security.EPSGrowthRaw;
        }
    }
}
