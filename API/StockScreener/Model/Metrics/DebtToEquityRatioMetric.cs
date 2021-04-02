using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class DebtToEquityRatioMetric : IMetric
    {
        public DebtToEquityRatioMetric(List<Range> ranges)
        {
            debtToEquityRatio = ranges;
        }

        private List<Range> debtToEquityRatio { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !debtToEquityRatio.Any(range => range.WithinRange(security.DebtToEquityRatio)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.DebtToEquityRatio;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.DebtToEquityRatio };
        }
    }
}
