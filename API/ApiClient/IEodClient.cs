using ApiClient.Models.Eod;
using System.Collections.Generic;

namespace ApiClient
{
	public interface IEodClient
	{
		EodIndex GetIndexInfo(string exchange);
		List<EodCandle> GetPriceData(string ticker);
		List<EodDividend> GetDividendData(string ticker);
		EodFundementals GetFundementals(string ticker);
	}
}
