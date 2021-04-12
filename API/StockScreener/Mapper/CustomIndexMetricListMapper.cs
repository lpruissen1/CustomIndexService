using Database.Model.User.CustomIndices;
using StockScreener.Model.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using UserCustomIndices.Database.Model.User.CustomIndices;

namespace StockScreener.Mapper
{
    public class CustomIndexMapper : IMetricListMapper<CustomIndex>
    {
        private Dictionary<Type, Func<Rule, IMetric>> metricActionMapper = new Dictionary<Type, Func<Rule, IMetric>>();

        public CustomIndexMapper()
        {
            metricActionMapper.Add(typeof(Sector), MapSectorsAndIndustries);
            metricActionMapper.Add(typeof(MarketCapitalization), MapMarketCap);
            metricActionMapper.Add(typeof(RevenueGrowthAnnualized), MapRevenueGrowthAnnualized);
            metricActionMapper.Add(typeof(CoefficientOfVariation), MapCoefficientOfVariation);
            metricActionMapper.Add(typeof(EPSGrowthAnnualized), MapEPSGrowthAnnualized);
            metricActionMapper.Add(typeof(AnnualizedTrailingPerformance), MapTrailingPerformanceAnnualized);
            metricActionMapper.Add(typeof(PriceToEarningsRatioTTM), MapPriceToEarningsRatioTTM);
            metricActionMapper.Add(typeof(PriceToSalesRatioTTM), MapPriceToSalesRatioTTM);
            metricActionMapper.Add(typeof(DividendYield), MapDividendYield);
        }

        public MetricList MapToMetricList(CustomIndex index)
        {
            MetricList metricList = new MetricList();
            metricList.Indices = index.Markets.ToArray();

            metricList.AddRange(index.Rules.Select(rule => metricActionMapper[rule.GetType()].Invoke(rule)));

            return metricList;
        }

        private IMetric MapSectorsAndIndustries(Rule rule)
        {
            var sectors = rule as Sector;

            return new SectorAndIndustryMetric(sectors.Sectors, sectors.Industries);
        }

        private IMetric MapMarketCap(Rule rule)
        {
            return MapRangedRule<MarketCapMetric>(rule as RangedRule);
        }

        private IMetric MapPriceToSalesRatioTTM(Rule rule)
        {
            return MapRangedRule<PriceToSalesRatioTTMMetric>(rule as RangedRule);
        }

        private IMetric MapDividendYield(Rule rule)
        {
            return MapRangedRule<DividendYieldMetric>(rule as RangedRule);
        }

        private IMetric MapPriceToEarningsRatioTTM(Rule rule)
        {
            return MapRangedRule<PriceToEarningsRatioTTMMetric>(rule as RangedRule);
        }

        private IMetric MapRevenueGrowthAnnualized(Rule rule)
        {
            return MapTimeRangedRule<RevenueGrowthAnnualizedMetric>(rule as TimedRangeRule);
        }

        private IMetric MapCoefficientOfVariation(Rule rule)
        {
            return MapTimeRangedRule<CoefficientOfVariationMetric>(rule as TimedRangeRule);
        }

        private IMetric MapEPSGrowthAnnualized(Rule rule)
        {
            return MapTimeRangedRule<EPSGrowthAnnualizedMetric>(rule as TimedRangeRule);
        }

        private IMetric MapTrailingPerformanceAnnualized(Rule rule)
        {
            return MapTimeRangedRule<TrailingPerformanceAnnualizedMetric>(rule as TimedRangeRule);
        }

        private TMetricType MapRangedRule<TMetricType>(RangedRule rule)
        {
            var list = new List<Core.Range>();

            foreach (var range in rule.Ranges)
            {
                list.Add(new Core.Range(range.Upper, range.Lower));
            }

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }

        private TMetricType MapTimeRangedRule<TMetricType>(TimedRangeRule rule)
        {
            var list = new List<RangeAndTimePeriod>();

            foreach (var timedRange in rule.TimedRanges)
            {
                list.Add(new RangeAndTimePeriod(new Core.Range(timedRange.Upper, timedRange.Lower), timedRange.TimePeriod));
            }

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }
    }

    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
