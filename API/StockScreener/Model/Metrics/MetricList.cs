using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
	public class MetricList : IMetric
	{
		private string[] indices { get; init; }
		private List<IMetric> metrics { get; init; } = new List<IMetric>();
		private IInclusionExclusionHandler inclusionExclusionHandler { get; init; }

		public MetricList(string[] indices, List<IMetric> metrics, IInclusionExclusionHandler inclusionExclusionHandler)
		{
			this.indices = indices;
			this.metrics = metrics;
			this.inclusionExclusionHandler = inclusionExclusionHandler;
		}

		public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            foreach (var metric in metrics)
            {
                metric.Apply(ref securitiesList);
			}

			inclusionExclusionHandler.Apply(ref securitiesList);
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

        public SecuritiesSearchParams GetSearchParams()
        {
			return new SecuritiesSearchParams { Markets = indices, Datapoints = GetBaseDatapoints() };
        }
    }
}
