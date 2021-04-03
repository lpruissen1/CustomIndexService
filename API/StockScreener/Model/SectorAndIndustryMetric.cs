using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model
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

        public void Apply(ref SecuritiesList securitiesList)
        {
            securitiesList.RemoveAll(s => !industries.Contains(s.Industry) && !sectors.Contains(s.Sector));
        }

        public IEnumerable<Datapoint> GetRelevantDatapoints()
        {
            yield return Datapoint.Industry;
            yield return Datapoint.Sector;
        }
    }
}
