using Core;
using Database.Core;
using StockScreener.Database.Model;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IMonthPriceDataRepository : IBaseRepository<MonthPriceData>
	{
		void LoadPriceData(List<MonthPriceData> priceData);
		void UpdatePriceData(MonthPriceData priceData);
		Dictionary<string, IEnumerable<MonthPriceData>> GetPriceData(IEnumerable<string> tickers, TimePeriod timePeriod);
	}
}
