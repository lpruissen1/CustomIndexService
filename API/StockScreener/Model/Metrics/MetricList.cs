using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public class MetricList : IMetric
    {
        public string[] Indices { get; set; }
        private List<IMetric> metrics { get; init; } = new List<IMetric>();

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            foreach (var metric in metrics)
            {
                metric.Apply(ref securitiesList);
            }
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            foreach (var metric in metrics)
            {
                foreach (var datapoint in metric.GetBaseDatapoints())
                {
                    yield return datapoint;
                }
            }
        }

        public void Add(IMetric metric)
        {
            metrics.Add(metric);
        }

        public void AddRange(IEnumerable<IMetric> metric)
        {
            metrics.AddRange(metric);
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            foreach (var metric in metrics)
            {
                foreach (var datapoint in metric.GetDerivedDatapoints())
                {
                    yield return datapoint;
                }
            };
        }
    }
}
