using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
	public class SectorAndIndustryMetric : IMetric
    {
        public SectorAndIndustryMetric(List<string> sectors, List<string> industries)
        {
            this.industries = industries;
            this.sectors = sectors;
        }

        private List<string> industries { get; init; }
        private List<string> sectors { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(s => RemoveCheckIndustry(s.Industry) && CheckSector(s.Sector));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Industry;
            yield return BaseDatapoint.Sector;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { Rule = RuleType.SectorIndustry };
		}

		public TimePeriod? GetPriceTimePeriod()
		{
			return null;
		}

		private bool RemoveCheckIndustry(string industry)
		{
			if (!industries.Any())
				return true;

			return !industries.Contains(industry);
		}

		private bool CheckSector(string sector)
		{
			if (!sectors.Any())
				return true;

			return !sectors.Contains(sector);

		}
	}
}
