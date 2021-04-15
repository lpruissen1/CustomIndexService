using StockScreener.Model.Metrics;
using System.Collections.Generic;
using UserCustomIndices.Model.Response;

namespace StockScreener.Mapper
{
    public class CustomIndexResponseMapper : IMetricListMapper<CustomIndexResponse>
    {
        public MetricList MapToMetricList(CustomIndexResponse input)
        {
            MetricList metricList = new MetricList();
            metricList.Indices = input.Markets.ToArray();

            metricList.Add(new SectorAndIndustryMetric(input.Sectors ?? new List<string>(), input.Industries ?? new List<string>()));

            return metricList;
        }
    }
}
