using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
	public class InclusionExclusionHandler : IInclusionExclusionHandler
	{
		private readonly List<string> exclusions;
		private readonly List<string> inclusions;

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
				if(!securitiesList.Any(security => security.Ticker == ticker))
					securitiesList.Add(new DerivedSecurity { Ticker = ticker });
			}
		}
	}
}
