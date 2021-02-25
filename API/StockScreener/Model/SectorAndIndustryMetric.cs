using System.Linq;

namespace StockScreener
{
    public class SectorAndIndustryMetric : IMetric
    {
        public string sector { get; init; }
        public string[] industries { get; init; }

        public SecuritiesList Apply(SecuritiesList securitiesList)
        {
            if ( industries.Length == 0 )
                return securitiesList.Where(s => s.sector == sector);

            return securitiesList.Where(s => industries.Contains(s.industry));
        }
    }
}
