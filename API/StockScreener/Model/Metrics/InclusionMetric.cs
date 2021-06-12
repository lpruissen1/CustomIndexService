using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
	public class InclusionMetric : IMetric
    {
        public InclusionMetric(List<string> includedTickers)
        {
			this.includedTickers = includedTickers;
        }

        private List<string> includedTickers { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
			var tempSecurityList = securitiesList;
			var newSecurities = includedTickers.Where(includedTicker => !tempSecurityList.Any(security =>  includedTicker == security.Ticker));

			foreach (var ticker in newSecurities)
			{
				securitiesList.Add(new DerivedSecurity { Ticker = ticker });
			}
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
