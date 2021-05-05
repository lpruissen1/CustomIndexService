//using Database.Model.User.CustomIndices;
//using StockScreener.Model.Metrics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UserCustomIndices.Core;
//using UserCustomIndices.Database.Model.User.CustomIndices;

//namespace StockScreener.Mapper
//{
//    public class CustomIndexMapper : IMetricListMapper<CustomIndex>
//    {
//        private Dictionary<Type, Func<RuleType, IMetric>> metricActionMapper = new Dictionary<Type, Func<CustomIndexRule, IMetric>>();

//        public CustomIndexMapper()
//        {
//            metricActionMapper.Add(RuleType.SectorIndustry, MapSectorsAndIndustries);
//            metricActionMapper.Add(typeof(Mark), MapMarketCap);
//            metricActionMapper.Add(typeof(RevenueGrowthAnnualized), MapRevenueGrowthAnnualized);
//            metricActionMapper.Add(typeof(CoefficientOfVariation), MapCoefficientOfVariation);
//            metricActionMapper.Add(typeof(EPSGrowthAnnualized), MapEPSGrowthAnnualized);
//            metricActionMapper.Add(typeof(AnnualizedTrailingPerformance), MapTrailingPerformanceAnnualized);
//            metricActionMapper.Add(typeof(PriceToEarningsRatioTTM), MapPriceToEarningsRatioTTM);
//            metricActionMapper.Add(typeof(PriceToSalesRatioTTM), MapPriceToSalesRatioTTM);
//            metricActionMapper.Add(typeof(DividendYield), MapDividendYield);
//        }

//        public MetricList MapToMetricList(CustomIndex index)
//        {
//            var metricList = new MetricList();
//            metricList.Indices = index.Markets.ToArray();
//            metricList.Add(new SectorAndIndustryMetric(index.Sector.Sectors, index.RangedRule));
//            metricList.AddRange(index.Rules.Select(rule => metricActionMapper[rule.GetType()].Invoke(rule)));

//            return metricList;
//        }

//        private IMetric MapSectorsAndIndustries(CustomIndexRule rule)
//        {
//            var sectors = rule as Sector;

//            return new SectorAndIndustryMetric(sectors.Sectors, sectors.Industries);
//        }

//        private IMetric MapMarketCap(CustomIndexRule rule)
//        {
//            return MapRangedRule<MarketCapMetric>(rule as RangedRule);
//        }

//        private IMetric MapPriceToSalesRatioTTM(CustomIndexRule rule)
//        {
//            return MapRangedRule<PriceToSalesRatioTTMMetric>(rule as RangedRule);
//        }

//        private IMetric MapDividendYield(CustomIndexRule rule)
//        {
//            return MapRangedRule<DividendYieldMetric>(rule as RangedRule);
//        }

//        private IMetric MapPriceToEarningsRatioTTM(CustomIndexRule rule)
//        {
//            return MapRangedRule<PriceToEarningsRatioTTMMetric>(rule as RangedRule);
//        }

//        private IMetric MapRevenueGrowthAnnualized(CustomIndexRule rule)
//        {
//            return MapTimeRangedRule<RevenueGrowthAnnualizedMetric>(rule as TimedRangeRule);
//        }

//        private IMetric MapCoefficientOfVariation(CustomIndexRule rule)
//        {
//            return MapTimeRangedRule<CoefficientOfVariationMetric>(rule as TimedRangeRule);
//        }

//        private IMetric MapEPSGrowthAnnualized(CustomIndexRule rule)
//        {
//            return MapTimeRangedRule<EPSGrowthAnnualizedMetric>(rule as TimedRangeRule);
//        }

//        private IMetric MapTrailingPerformanceAnnualized(CustomIndexRule rule)
//        {
//            return MapTimeRangedRule<TrailingPerformanceAnnualizedMetric>(rule as TimedRangeRule);
//        }

//        private TMetricType MapRangedRule<TMetricType>(RangedRule rule)
//        {
//            var list = new List<Core.Range>();

//            foreach (var range in rule.Ranges)
//            {
//                if (range.Upper == 0)
//                    list.Add(new Core.Range(range.Upper, range.Lower));
//                else
//                    list.Add(new Core.Range(double.MaxValue, range.Lower));
//            }

//            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
//        }

//        private TMetricType MapTimeRangedRule<TMetricType>(TimedRangeRule rule)
//        {
//            var list = new List<RangeAndTimePeriod>();

//            foreach (var timedRange in rule.TimedRanges)
//            {
//                if(timedRange.Upper == 0)
//                    list.Add(new RangeAndTimePeriod(new Core.Range(timedRange.Upper, timedRange.Lower), timedRange.TimePeriod));
//                else
//                    list.Add(new RangeAndTimePeriod(new Core.Range(double.MaxValue, timedRange.Lower), timedRange.TimePeriod));
//            }

//            return (TMetricType)Activator.CreateInstance(typeof(TMetricType), list);
//        }
//    }
//}
