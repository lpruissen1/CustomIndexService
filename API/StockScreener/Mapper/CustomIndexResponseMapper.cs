using StockScreener.Model.Metrics;
using UserCustomIndices.Model.Response;
using System.Linq;
using System.Collections.Generic;
using UserCustomIndices.Core.Model;
using UserCustomIndices.Core;
using System;

namespace StockScreener.Mapper
{
    public class CustomIndexResponseMapper : IMetricListMapper<CustomIndexResponse>
    {
        private Dictionary<RuleType, Func<TimedRangeRule, IMetric>> timedRangedRuleMetricMapper = new Dictionary<RuleType, Func<TimedRangeRule, IMetric>>();
        private Dictionary<RuleType, Func<RangedRule, IMetric>> rangedRuleMetricMapper = new Dictionary<RuleType, Func<RangedRule, IMetric>>();

        public CustomIndexResponseMapper()
        {
            timedRangedRuleMetricMapper = new Dictionary<RuleType, Func<TimedRangeRule, IMetric>>
            {
                {RuleType.AnnualizedTraillingPerformance, MapTrailingPerformanceAnnualized },
                {RuleType.EPSGrowthAnnualized, MapEPSGrowthAnnualized },
                {RuleType.CoefficientOfVariation, MapCoefficientOfVariation },
                {RuleType.RevenueGrowthAnnualized, MapRevenueGrowthAnnualized }
            };

            rangedRuleMetricMapper = new Dictionary<RuleType, Func<RangedRule, IMetric>>
            {
                {RuleType.PriceToSalesRatioTTM, MapPriceToSalesRatioTTM },
                {RuleType.MarketCap, MapMarketCap },
                {RuleType.DividendYield, MapDividendYield },
                {RuleType.PriceToEarningsRatioTTM, MapPriceToEarningsRatioTTM }
            };
        }

        public MetricList MapToMetricList(CustomIndexResponse input)
        {
            MetricList metricList = new MetricList();
            metricList.Indices = input.Markets.ToArray();

            if (input.Sectors.Any() || input.Industries.Any())
                metricList.Add(new SectorAndIndustryMetric(input.Sectors, input.Industries));   

            foreach(var rangedRule in input.RangedRule)
            {
                metricList.Add(rangedRuleMetricMapper[rangedRule.RuleType].Invoke(rangedRule));
            }

            foreach(var timedRangeRule in input.TimedRangeRule)
            {
                metricList.Add(timedRangedRuleMetricMapper[timedRangeRule.RuleType].Invoke(timedRangeRule));
            }

            return metricList;
        }

        private IMetric MapMarketCap(RangedRule rule)
        {
            return MapRangedRule<MarketCapMetric>(rule);
        }

        private IMetric MapPriceToSalesRatioTTM(RangedRule rule)
        {
            return MapRangedRule<PriceToSalesRatioTTMMetric>(rule);
        }

        private IMetric MapDividendYield(RangedRule rule)
        {
            return MapRangedRule<DividendYieldMetric>(rule );
        }

        private IMetric MapPriceToEarningsRatioTTM(RangedRule rule)
        {
            return MapRangedRule<PriceToEarningsRatioTTMMetric>(rule);
        }

        private IMetric MapRevenueGrowthAnnualized(TimedRangeRule rule)
        {
            return MapTimeRangedRule<RevenueGrowthAnnualizedMetric>(rule);
        }

        private IMetric MapCoefficientOfVariation(TimedRangeRule rule)
        {
            return MapTimeRangedRule<CoefficientOfVariationMetric>(rule);
        }

        private IMetric MapEPSGrowthAnnualized(TimedRangeRule rule)
        {
            return MapTimeRangedRule<EPSGrowthAnnualizedMetric>(rule);
        }

        private IMetric MapTrailingPerformanceAnnualized(TimedRangeRule rule)
        {
            return MapTimeRangedRule<TrailingPerformanceAnnualizedMetric>(rule);
        }

        private TMetricType MapRangedRule<TMetricType>(RangedRule rule)
        {
            var list = new List<Core.Range>();

            if (rule.Upper == 0)
                list.Add(new Core.Range(rule.Upper, rule.Lower));
            else
                list.Add(new Core.Range(double.MaxValue, rule.Lower));

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }

        private TMetricType MapTimeRangedRule<TMetricType>(TimedRangeRule rule)
        {
            var list = new List<RangeAndTimePeriod>();

            if (rule.Upper == 0)
                list.Add(new RangeAndTimePeriod(new Core.Range(rule.Upper, rule.Lower), rule.TimePeriod));
            else
                list.Add(new RangeAndTimePeriod(new Core.Range(double.MaxValue, rule.Lower), rule.TimePeriod));

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }
    }
}
