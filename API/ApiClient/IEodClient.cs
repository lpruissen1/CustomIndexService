using ApiClient.Models.Eod;
using System.Collections.Generic;

namespace ApiClient
{
	public interface IEodClient
	{
		EodIndex GetIndexInfo(string exchange);
		List<EodCandle> GetPriceData(string ticker);
		EodCompanyInfo GetCompanyInfo(string ticker);
		EodEarnings GetEarnings(string ticker);
		EodOutstandingShares GetOutstandingShares(string ticker);
		EodBalanceSheet GetBalanceSheet(string ticker);
		EodIncomeStatement GetIncomeStatement(string ticker);
		EodCashFlow GetCashFlow(string ticker);

	}
}
