using StockScreener.Model.Metrics;
using System.Linq;
using System.Collections.Generic;
using System;
using Core;
using StockScreener.Core.Request;

namespace StockScreener.Mapper
{
	public class ScreeningRequestMapper : IMetricListMapper<ScreeningRequest>
    {
        private Dictionary<RuleType, Func<TimedRangeRule, IMetric>> timedRangedRuleMetricMapper;
        private Dictionary<RuleType, Func<RangedRule, IMetric>> rangedRuleMetricMapper;

        public ScreeningRequestMapper()
        {
            timedRangedRuleMetricMapper = new Dictionary<RuleType, Func<TimedRangeRule, IMetric>>
            {
                {RuleType.AnnualizedTrailingPerformance, MapTrailingPerformanceAnnualized },
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

        public MetricList MapToMetricList(ScreeningRequest input)
        {
			var metricList = new List<IMetric>();

            if (input.Sectors.Any() || input.Industries.Any())
                metricList.Add(new SectorAndIndustryMetric(input.Sectors, input.Industries));   

			foreach (var rangedRule in input.RangedRule)
            {
                metricList.Add(rangedRuleMetricMapper[rangedRule.RuleType].Invoke(rangedRule));
            }

            foreach(var timedRangeRule in input.TimedRangeRule)
            {
                metricList.Add(timedRangedRuleMetricMapper[timedRangeRule.RuleType].Invoke(timedRangeRule));
            }

			var inclusionExclusionHandler = new InclusionExclusionHandler(input.Inclusions, input.Exclusions);

			return new MetricList(input.Markets.ToArray(), metricList, inclusionExclusionHandler);
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
            return MapRangedRule<DividendYieldMetric>(rule);
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
                list.Add(new Core.Range(double.MaxValue, rule.Lower));
            else
                list.Add(new Core.Range(rule.Upper, rule.Lower));

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }

        private TMetricType MapTimeRangedRule<TMetricType>(TimedRangeRule rule)
        {
            var list = new List<RangeAndTimePeriod>();

            if (rule.Upper == 0)
                list.Add(new RangeAndTimePeriod(new Core.Range(double.MaxValue, rule.Lower), rule.TimePeriod));
            else
                list.Add(new RangeAndTimePeriod(new Core.Range(rule.Upper, rule.Lower), rule.TimePeriod));

            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
        }
    }
}
