
using AggregationService.Core;
using ApiClient.Models;

namespace ApiClient
{
	public interface IPolygonClient
	{
		PolygonCompanyInfoResponse GetCompanyInfo(string ticker);
		PolygonPriceDataResponse GetPriceData(string ticker, int interval, TimeResolution timeResolution);
		PolygonStockFinancialsResponse GetStockFinancials(string ticker);
	}
}
