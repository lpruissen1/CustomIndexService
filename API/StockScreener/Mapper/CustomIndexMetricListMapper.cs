using Database.Model.User.CustomIndices;
using StockScreener.Model;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Mapper
{
    public class CustomIndexMapper : IMetricListMapper<CustomIndex>
    {
        public MetricList MapToMetricList(CustomIndex index)
        {
            MetricList metricList = new MetricList();
            metricList.Add(MapStockIndices(index.Markets));
            metricList.AddRange(MapSectors(index.SectorAndIndsutry));

            return metricList;
        }

        private IEnumerable<SectorAndIndustryMetric> MapSectors(Sectors sectors)
        {
            if(sectors.IsNotNull())
                return sectors.SectorGroups.Select(sectors => new SectorAndIndustryMetric { sector = sectors.Name, industries = sectors.Industries });

            return Enumerable.Empty<SectorAndIndustryMetric>();
        }

        private IMetric MapStockIndices(ComposedMarkets markets)
        {
            return new MarketMetric { markets = markets.Markets };
        }
    }

    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
