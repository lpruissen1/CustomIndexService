using Database.Core;
using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IStockFinancialsRepository : IBaseRepository<StockFinancials>
    {
        IEnumerable<StockFinancials> Get(IEnumerable<string> tickers, IEnumerable<Datapoint> dataPoints);
    }
}
