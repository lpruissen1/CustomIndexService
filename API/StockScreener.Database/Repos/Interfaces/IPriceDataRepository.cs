using Core;
using Database.Core;
using StockScreener.Database.Model.Price;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IPriceDataRepository : IBaseRepository<PriceData>
	{
		void Update(HourPriceData obj);
		void Update(DayPriceData obj);
		double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
		List<TPriceEntry> GetMany<TPriceEntry>(IEnumerable<string> tickers) where TPriceEntry : PriceData;
		List<TPriceEntry> GetMany<TPriceEntry>(IEnumerable<string> tickers, TimePeriod timePeriod) where TPriceEntry : PriceData;
		IEnumerable<TPriceEntry> GetClosePriceOverTimePeriod<TPriceEntry>(IEnumerable<string> tickers, TimePeriod timeSpan) where TPriceEntry : PriceData;
		List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
	}
}
