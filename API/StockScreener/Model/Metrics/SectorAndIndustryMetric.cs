using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

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
            securitiesList.RemoveAll(s => !industries.Contains(s.Industry) && !sectors.Contains(s.Sector));
        }

        public IEnumerable<BaseDatapoint> GetBaseDatapoints()
        {
            yield return BaseDatapoint.Industry;
            yield return BaseDatapoint.Sector;
        }

        public IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints()
        {
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.Sector};
            yield return new DerivedDatapointConstructionData { datapoint = DerivedDatapoint.Industry};
        }
    }
}
