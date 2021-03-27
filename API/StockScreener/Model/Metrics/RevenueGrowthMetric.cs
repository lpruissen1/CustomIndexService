using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class RevenueGrowthMetric : IMetric
    {
        private List<RevenueGrowthMetricEntry> entries;

        public RevenueGrowthMetric(List<RevenueGrowthMetricEntry> revenueGrowthTargets)
        {
            entries = revenueGrowthTargets;
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(s => !entries.Any(entry => entry.Valid(s)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Revenue;
        }

        public IEnumerable<BaseDatapoint> GetDerivedDatapoints()
        {
            throw new System.NotImplementedException();
        }
    }
}
