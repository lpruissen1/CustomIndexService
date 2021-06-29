using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Core.Request;
using StockScreener.Core.Response;
using StockScreener.Interfaces;
using StockScreener.Mapper;
using StockScreener.Model.BaseSecurity;
using System.Linq;

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
			var securities = securitiesGrabber.GetSecuritiesByIndex(queryParams);

			var derivedDatapointCalculator = new DerivedDatapointCalculator();

			var derivedSecurityList = derivedDatapointCalculator.Derive(securities, metricList.GetDerivedDatapoints());

			metricList.Apply(ref derivedSecurityList);

			return derivedSecurityList;
		}

		public WeightingResponse Weighting(WeightingRequest request)
		{
			var weightCalculatorRepository = new WeightCalculatorRepository();
			var calculator = weightCalculatorRepository.GetWeightCalculator(request);
			var baseDatapoints = calculator.GetBaseDatapoint();

			var baseSecurities = securitiesGrabber.GetSecuritiesByTicker(request.Tickers, baseDatapoints);

			var derivedDatapointCalculator = new DerivedDatapointCalculator();

			var derivedSecurityList = derivedDatapointCalculator.Derive(baseSecurities, calculator.GetDerivedDatapointConstructionData());

			var result = calculator.Weight(derivedSecurityList);

			return new WeightingResponse()
			{
				Tickers = result
			};

		}
	}
}
