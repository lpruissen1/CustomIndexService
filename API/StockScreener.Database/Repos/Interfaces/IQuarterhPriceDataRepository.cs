using Core;
using Database.Core;
using StockScreener.Database.Model;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IQuarterhPriceDataRepository : IBaseRepository<QuarterPriceData>
	{
		void LoadPriceData(List<QuarterPriceData> priceData);
		void UpdatePriceData(QuarterPriceData priceData);
		Dictionary<string, IEnumerable<QuarterPriceData>> GetPriceData(IEnumerable<string> tickers, TimePeriod timePeriod);
	}
}
