using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class EPSGrowthAnnualizedMetric : RangeAndTimeSpanMetric
    {
        public EPSGrowthAnnualizedMetric(List<RangeAndTimeSpan> rangesAndTimeSpans) : base(rangesAndTimeSpans) {}

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.QuarterlyEarningsPerShare;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach(var entry in rangedDatapoint.GroupBy(x => x.GetTimeSpan()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.EPSGrowthAnnualized, Time = entry.GetTimeSpan() };
            }
        }

        public override Dictionary<TimeSpan, double> GetValue(DerivedSecurity security)
        {
            return security.EPSGrowthAnnualized;
        }
    }
}