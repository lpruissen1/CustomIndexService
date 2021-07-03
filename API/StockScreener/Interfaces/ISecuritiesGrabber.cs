using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener.Interfaces
{
	public interface ISecuritiesGrabber
	{
		SecuritiesList<BaseSecurity> GetSecuritiesByIndex(SecuritiesSearchParams searchParams);
		SecuritiesList<BaseSecurity> GetSecuritiesByTicker(List<string> tickers, IEnumerable<BaseDatapoint> datapoints);
	}
}
