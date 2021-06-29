using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class DebtToEquityRatioMetric : RangedMetric
    {
        public DebtToEquityRatioMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.DebtToEquityRatio;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.DebtToEquityRatio };
        }

        public override double? GetValue(DerivedSecurity security)
        {
            return security.DebtToEquityRatio;
        }
    }
}
