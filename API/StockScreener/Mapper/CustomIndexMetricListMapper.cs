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
            metricList.Indices = index.Markets.Markets;

            metricList.Add(MapSectorsAndIndustries(index.SectorAndIndsutry));
            metricList.Add(MapMarketCap(index.MarketCaps));

            return metricList;
        }

        private IMetric MapSectorsAndIndustries(Sectors sectors)
        {
            if ( sectors.IsNull() )
                return null;

            return null;
        }

        private IMetric MapMarketCap(MarketCaps marketCaps)
        {
            if(marketCaps.IsNull())
                return null;

            var list = new List<Range>();

            foreach(var marketCapRange in marketCaps.MarketCapGroups)
            {
                list.Add(new Range(marketCapRange.Upper, marketCapRange.Lower));
            }

            return new MarketCapMetric(list);
        }
    }

    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
