using Database.Model.User.CustomIndices;
using StockScreener.Model;
using System;
using System.Linq;
using StockScreener;
using Database.Model.User;
using System.Collections.Generic;

namespace StockScreener.MetricListMapper
{
    public class MetricListMapper : IMetricListMapper
    {
        public MetricList CustomIndexToMetricList(CustomIndex index)
        {
            MetricList metricList = new MetricList();

            metricList.AddRange(Map(index.SectorAndIndsutry.SectorGroups));

            return metricList;
        }

        private IEnumerable<IMetric> Map(Sector[] sectors)
        {
            return sectors.Select(sectors => new SectorAndIndustryMetric { sector = sectors.Name, industries = sectors.Industries } as IMetric).AsEnumerable();
        }
    }

    public interface IMetricListMapper
    {
        MetricList CustomIndexToMetricList(CustomIndex index);
    }
}
