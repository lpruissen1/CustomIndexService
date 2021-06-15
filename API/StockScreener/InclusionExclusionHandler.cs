using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
	public class InclusionExclusionHandler : IInclusionExclusionHandler
	{
		private List<string> exclusions;
		private List<string> inclusions;

		public InclusionExclusionHandler(List<string> inclusions, List<string> exclusions)
		{
			this.inclusions = inclusions;
			this.exclusions = exclusions;
		}

		public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList) 
		{
			securitiesList.RemoveAll(security => exclusions.Any(ticker => ticker == security.Ticker));

			foreach (var ticker in inclusions)
			{
				securitiesList.Add(new DerivedSecurity { Ticker = ticker });
			}
		}
	}
}
