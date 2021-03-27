using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (metric is null)
                return;

            metrics.Add(metric);
        }

        public IEnumerable<BaseDatapoint> GetDerivedDatapoints()
        {
            throw new NotImplementedException();
        }
    }
}
