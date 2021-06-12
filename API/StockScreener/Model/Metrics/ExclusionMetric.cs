using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
	public class ExclusionMetric : IMetric
    {
        public ExclusionMetric(List<string> excludedTickers)
        {
			this.excludedTickers = excludedTickers;
        }

        private List<string> excludedTickers { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => excludedTickers.Any(ticker => ticker == security.Ticker));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
		{
			return Enumerable.Empty<BaseDatapoint>();
		}

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
		{
			return Enumerable.Empty<DerivedDatapointConstructionData>();
		}
    }
}
