using StockScreener.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model
{
    public class MetricList : IMetric
    {
        public string[] Indices { get; set; }
        private List<IMetric> metrics { get; init; } = new List<IMetric>();

        public void Apply(ref SecuritiesList securitiesList)
        {
            foreach(var metric in metrics)
            {
                metric.Apply(ref securitiesList);
            }
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            foreach(var metric in metrics)
            {
                foreach(var datapoint in metric.GetRelevantDatapoints())
                {
                    yield return datapoint;
                }
            }
        }

        public void Add(IMetric metric)
        {
            if(metric is null)
                return;

            metrics.Add(metric);
        }
    }
}
