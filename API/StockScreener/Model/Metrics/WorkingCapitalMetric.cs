using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public class WorkingCapitalMetric : IMetric
    {
        public WorkingCapitalMetric(List<Range> ranges)
        {
            workingCapital = ranges;
        }

        private List<Range> workingCapital { get; init; }

        public void AddWorkingCapital(Range workingCapitalRange)
        {
            workingCapital.Add(workingCapitalRange);
        }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !workingCapital.Any(range => range.WithinRange(security.WorkingCapital)));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.WorkingCapital;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.WorkingCapital };
        }
    }
}
