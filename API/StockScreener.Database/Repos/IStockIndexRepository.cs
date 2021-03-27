using Database.Core;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IStockIndexRepository : IBaseRepository<StockIndex>
    {
        IEnumerable<string> Get(IEnumerable<string> indices);
    }
}
