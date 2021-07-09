using Database.Core;
using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IStockFinancialsRepository : IBaseRepository<StockFinancials>
    {
		StockFinancials Get(string tickers, IEnumerable<BaseDatapoint> dataPoints);
		IEnumerable<StockFinancials> GetMany(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> dataPoints);
    }
}
