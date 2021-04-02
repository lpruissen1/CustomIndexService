using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class PriceToEarningsRatioTTMMetric : RangedMetric
    {
        public PriceToEarningsRatioTTMMetric(List<Range> ranges) : base(ranges) { }

        public override IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Price;
            yield return BaseDatapoint.QuarterlyEarningsPerShare;
        }

        public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.PriceToEarningsRatioTTM };
        }

        public override double GetValue(DerivedSecurity security)
        {
            return security.PriceToEarningsRatioTTM;
        }
    }
}
