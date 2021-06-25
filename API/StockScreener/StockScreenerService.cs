using StockScreener.Calculators;
using StockScreener.Core.Request;
using StockScreener.Interfaces;
using StockScreener.Mapper;
using StockScreener.Model.BaseSecurity;

namespace StockScreener
{
	public class StockScreenerService : IStockScreenerService
    {
        private readonly ISecuritiesGrabber securitiesGrabber;

        public StockScreenerService(ISecuritiesGrabber securitiesGrabber)
        {
            this.securitiesGrabber = securitiesGrabber;
		}

        public SecuritiesList<DerivedSecurity> Screen(ScreeningRequest request)
        {
            var mapper = new ScreeningRequestMapper();
            var metricList = mapper.MapToMetricList(request);

			var queryParams = metricList.GetSearchParams();
            var securities = securitiesGrabber.GetSecurities(queryParams);

            var derivedDatapointCalculator = new DerivedDatapointCalculator();

            var derivedSecurityList = derivedDatapointCalculator.Derive(securities, metricList.GetDerivedDatapoints());

            metricList.Apply(ref derivedSecurityList);

            return derivedSecurityList;
        }
    }
}
