using Database.Model.StockData;
using System.Collections.Generic;

namespace Database.Repositories
{
    public interface IStockIndexRepository : IBaseRepository<StockIndex>
    {
        IEnumerable<string> Get(IEnumerable<string> indices);
    }
}
