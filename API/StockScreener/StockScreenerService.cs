using Database.Model.User.CustomIndices;
using StockScreener.Calculators;
using StockScreener.Mapper;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;

namespace StockScreener
{
    public class StockScreenerService : IStockScreenerService
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
    }
}
