using Database.Model.User.CustomIndices;
using StockScreener.Core;
using StockScreener.Model.Metrics;
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
            metricList.Add(MapRevenueGrowth(index.RevenueGrowths));
            metricList.Add(MapPriceToEarningsTTM(index.PriceToEarningsRatioTTM));
            metricList.Add(MapPayoutRatio(index.PayoutRatio));


            return metricList;
        }

        private IMetric MapSectorsAndIndustries(Sectors sectors)
        {
            if (sectors.IsNull())
                return null;

            var sectorList = new List<string>();
            var industryList = new List<string>();

            foreach(var sectorGroup in sectors.SectorGroups)
            {
                if (sectorGroup.Industries is null)
                    sectorList.Add(sectorGroup.Name);

                else
                    industryList.AddRange(sectorGroup.Industries);
            }

            return new SectorAndIndustryMetric(sectorList, industryList);
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

        private IMetric MapPayoutRatio(List<PayoutRatios> payoutRatio)
        {
            if (!payoutRatio.Any())
                return null;

            var list = new List<Range>();

            foreach (var payoutRatioRange in payoutRatio)
            {
                list.Add(new Range(payoutRatioRange.Upper, payoutRatioRange.Lower));
            }

            return new PayoutRatioMetric(list);
        }

        private IMetric MapRevenueGrowth(List<RevenueGrowth> revenueGrowth)
        {
            if(!revenueGrowth.Any())
                return null;

            var list = new List<RangedEntry>();

            foreach(var revenueGrowthTarget in revenueGrowth)
            {
                list.Add(new RangedEntry(new Range(revenueGrowthTarget.Upper, revenueGrowthTarget.Lower), GetTimeSpan(revenueGrowthTarget.TimePeriod)));
            }

            return new RevenueGrowthMetric(list);
        }

        private IMetric MapPriceToEarningsTTM(List<PriceToEarningsRatioTTM> priceToEarningsRatioTTM)
        {
            if (!priceToEarningsRatioTTM.Any())
                return null;

            var list = new List<RangedEntry>();

            foreach (var priceToEarningsRatioTTMTarget in priceToEarningsRatioTTM)
            {
                list.Add(new RangedEntry(new Range(priceToEarningsRatioTTMTarget.Upper, priceToEarningsRatioTTMTarget.Lower)));
            }

            return new PriceToEarningsRatioTTMMetric(list);
        }

        private TimeSpan GetTimeSpan(int timeRange)
        {
            switch (timeRange)
            {
                case 1:
                    return TimeSpan.Quarterly;
                case 2:
                    return TimeSpan.HalfYear;
                case 4:
                    return TimeSpan.OneYear;
                case 12:
                    return TimeSpan.ThreeYear;
                case 20:
                    return TimeSpan.FiveYear;
                default:
                    throw new System.Exception("Fuck yourself");
            }
        }
    }

    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
