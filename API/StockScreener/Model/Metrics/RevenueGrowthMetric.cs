using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class RevenueGrowthMetric : IMetric
    {
        private List<RangeAndTimeSpan> entries;

        public RevenueGrowthMetric(List<RangeAndTimeSpan> revenueGrowthTargets)
        {
            entries = revenueGrowthTargets;
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(s => !entries.Any(entry => entry.Valid(s.RevenueGrowth[entry.GetTimeSpan()])));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Revenue;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach(var entry in entries.GroupBy(x => x.GetTimeSpan()).Select(x => x.FirstOrDefault()))
            {
                yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.RevenueGrowth, Time = entry.GetTimeSpan() };
            }
        }
    }
}
