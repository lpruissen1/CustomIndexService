using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class ProfitMarginMetric : IMetric
    {
        public ProfitMarginMetric(List<Range> ranges)
        {
            profitMargin = ranges;
        }

        private List<Range> profitMargin { get; init; }

        public void AddProfitMargin(Range profitMarginRange)
        {
            profitMargin.Add(profitMarginRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !profitMargin.Any(range => range.WithinRange(security.ProfitMargin)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.ProfitMargin;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.ProfitMargin };
        }
    }
}
