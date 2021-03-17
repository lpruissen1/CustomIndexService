using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public class SectorAndIndustryMetric : IMetric
    {
        public SectorAndIndustryMetric()
        {
            //industries = new List<string>();
            //sectors = new List<string>();
        }

        private List<string> industries { get; init; }
        private List<string> sectors { get; init; }

        public void AddSector(string sector)
        {
            sectors.Add(sector);
        }

        public void AddIndustry(string industry)
        {
            industries.Add(industry);
        }

        public void Apply(ref SecuritiesList securitiesList)
        {
            securitiesList.RemoveAll(s => !industries.Contains(s.Industry) || !sectors.Contains(s.Sector));
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            yield return Datapoint.Industry;
        }
    }
}
