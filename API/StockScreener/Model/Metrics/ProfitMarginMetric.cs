using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class ProfitMarginMetric : RangedMetric
    {
        public ProfitMarginMetric(List<Range> ranges) : base(ranges)
        {
        }

        public  override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.ProfitMargin;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.ProfitMargin };
        }

        public override double? GetValue(DerivedSecurity security)
        {
            return security.ProfitMargin;
        }
    }
}
