using Database.Core;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IStockIndexRepository : IBaseRepository<StockIndex>
	{
		IEnumerable<string> Get(IEnumerable<string> indices);
		public StockIndex GetIndex(string name);
		public void CreateEntryForIndex(string name, List<string> tickers);
	}
}
