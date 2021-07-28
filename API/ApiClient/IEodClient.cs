using ApiClient.Models.Eod;
using System.Collections.Generic;

namespace ApiClient
{
	public interface IEodClient
	{
		List<EodExchangeEntry> GetExhangeInfo(string exchange);
		List<EodCandle> GetPriceData(string ticker);
		EodCompanyInfo GetCompanyInfo(string ticker);
		EodEarnings GetEarnings(string ticker);
		EodOutstandingShares GetOutstandingShares(string ticker);

	}
}
