﻿using StockScreener.Calculators;
using StockScreener.Mapper;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using UserCustomIndices.Model.Response;

namespace StockScreener
{
    public class StockScreenerService : IStockScreenerService
    {
        private readonly ISecuritiesGrabber securitiesGrabber;

        public StockScreenerService(ISecuritiesGrabber securitiesGrabber)
        {
            this.securitiesGrabber = securitiesGrabber;
        }

        public SecuritiesList<DerivedSecurity> Screen(CustomIndexResponse index)
        {
            var mapper = new CustomIndexResponseMapper();
            var metricList = mapper.MapToMetricList(index);

            var queryParams = new SecuritiesSearchParams { Indices = metricList.Indices, Datapoints = metricList.GetBaseDatapoints() };
            var securities = securitiesGrabber.GetSecurities(queryParams);

            var derivedDatapointCalculator = new DerivedDatapointCalculator();

            var derivedSecurityList = derivedDatapointCalculator.Derive(securities, metricList.GetDerivedDatapoints());

            metricList.Apply(ref derivedSecurityList);
            
            return derivedSecurityList;
        }
    }
}
