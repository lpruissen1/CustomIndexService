using Database.Model.User.CustomIndices;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Mapper;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class StockScreenerService
    {
        private readonly ISecuritiesGrabber securitiesGrabber;

        public StockScreenerService(ISecuritiesGrabber securitiesGrabber)
        {
            this.securitiesGrabber = securitiesGrabber;
        }

        public SecuritiesList<DerivedSecurity> Screen(CustomIndex index)
        {
            var mapper = new CustomIndexMapper();
            var metricList = mapper.MapToMetricList(index);

            var queryParams = new SecuritiesSearchParams { Indices = metricList.Indices, Datapoints = metricList.GetBaseDatapoints() };
            var securities = securitiesGrabber.GetSecurities(queryParams);

            var derivedDatapointCalculator = new DerivedDatapointCalculator();

            var derivedSecurityList = derivedDatapointCalculator.Derive(securities, metricList.GetDerivedDatapoints());

            metricList.Apply(ref derivedSecurityList);

            return derivedSecurityList;
        }

        public Dictionary<string, double> Weight(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> weightingStrategy)
        {
            var securities = securitiesGrabber.GetSecurities(tickers, weightingStrategy);

            var total = securities.Sum(x => x.MarketCap);

            return securities.ToDictionary(security => security.Ticker, security => Math.Round(security.MarketCap / total, 5));
        }

        public class WeightingRequest<TWeightingStrategy>{
            public List<string> Tickers;
            public TWeightingStrategy strategy;
        }
    }
}
