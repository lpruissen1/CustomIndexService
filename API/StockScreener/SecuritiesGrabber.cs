using StockScreener.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class SecuritiesGrabber : ISecuritiesGrabber
    {
        private readonly Dictionary<Type, Func<IEnumerable<Datapoint>>> metricMapper;

        public SecuritiesGrabber()
        {
            metricMapper = new()
            {
                { typeof(SectorAndIndustryMetric), () => new Datapoint[] { Datapoint.Industry, Datapoint.Sector } }
            };
        }

        public SecuritiesList GetSecurities(MetricList metrics)
        {
            var datapoints = metrics.Select(metric => metricMapper[metric.GetType()]);

            return null;
        }
    }
}
