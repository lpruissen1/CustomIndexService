using StockScreener.Core;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model
{
    public class RevenueGrowthMetric : IMetric
    {
        private List<RevenueGrowthMetricEntry> entries;

        public RevenueGrowthMetric(List<RevenueGrowthMetricEntry> revenueGrowthTargets)
        {
            entries = revenueGrowthTargets;
        }

        public void Apply(ref SecuritiesList securitiesList)
        {
            securitiesList.RemoveAll(s => !entries.Any(entry => entry.Valid(s)));
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            yield return Datapoint.Revenue;
        }
    }
}
