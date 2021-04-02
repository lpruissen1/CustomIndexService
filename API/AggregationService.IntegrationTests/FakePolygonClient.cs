using AggregationService.Core;
using ApiClient;
using ApiClient.Models;

namespace AggregationService.IntegrationTests
{
	internal class FakePolygonClient : IPolygonClient
	{
		private PolygonStockFinancialsResponse stubStockFinancialsResponse;
		private PolygonCompanyInfoResponse stubCompanyInfoResponse;
		private PolygonPriceDataResponse stubPriceDataResponse;

		public FakePolygonClient(PolygonStockFinancialsResponse response)
		{
				stubStockFinancialsResponse = response;
		}

		public FakePolygonClient(PolygonCompanyInfoResponse response)
		{
			stubCompanyInfoResponse = response;
		}

		public FakePolygonClient(PolygonPriceDataResponse response)
		{
			stubPriceDataResponse = response;
		}

		public PolygonStockFinancialsResponse GetStockFinancials(string ticker)
		{
			return stubStockFinancialsResponse;
		}

		public PolygonCompanyInfoResponse GetCompanyInfo(string ticker)
		{
			return stubCompanyInfoResponse;
		}

		public PolygonPriceDataResponse GetPriceData(string ticker, int interval, TimeResolution timeResolution)
		{
			return stubPriceDataResponse;
		}
	}
}