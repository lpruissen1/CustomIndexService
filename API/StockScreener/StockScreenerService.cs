using Microsoft.Extensions.Logging;
using StockScreener.Calculators;
using StockScreener.Core.Request;
using StockScreener.Core.Response;
using StockScreener.Interfaces;
using StockScreener.Mapper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace StockScreener
{
	public class StockScreenerService : IStockScreenerService
    {
        private readonly ISecuritiesGrabber securitiesGrabber;
		private readonly ILogger logger;

		public StockScreenerService(ISecuritiesGrabber securitiesGrabber, ILogger logger)
        {
            this.securitiesGrabber = securitiesGrabber;
			this.logger = logger;
		}

        public ScreeningResponse Screen(ScreeningRequest request)
        {
			var stopwatch = Stopwatch.StartNew();

            var mapper = new ScreeningRequestMapper();
            var metricList = mapper.MapToMetricList(request);

			var queryParams = metricList.GetSearchParams();
			var securities = securitiesGrabber.GetSecuritiesByIndex(queryParams);

			var derivedDatapointCalculator = new DerivedDatapointCalculator();

			var derivedSecurityList = derivedDatapointCalculator.Derive(securities, metricList.GetDerivedDatapoints());

			metricList.Apply(ref derivedSecurityList);

			stopwatch.Stop();

			// this serialization needs to be moved to happen elsewhere
			var json = JsonSerializer.Serialize(request);

			logger.LogInformation(new EventId(1), $"Screening Request time in m/s: {stopwatch.ElapsedMilliseconds};  {json}");

			var newMapper = new ScreeningResponseMapper();

			return newMapper.Map(derivedSecurityList, metricList.GetDerivedDatapoints());
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

		public PurchaseOrderResponse GetPurchaseOrder(PurchaseOrderRequest request)
		{
			var screeningResult = Screen(request.ScreeningRequest);

			request.WeightingRequest.Tickers = screeningResult.Securities.Select(x => x.Ticker).ToList();

			var weightingResult = Weighting(request.WeightingRequest);

			var purchaseOrder = new PurchaseOrderResponse()
			{
				Tickers = weightingResult.Tickers.Select(x => new PurchaseOrderEntry()
				{
					Ticker = x.Ticker,
					Amount = Math.Round((decimal)(x.Weight / 100) * request.Amount, 2)
				}).ToList()
			};

			return purchaseOrder;
		}
	}
}
