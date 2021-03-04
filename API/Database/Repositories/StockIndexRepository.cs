using Database.Model.StockData;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Database.Repositories
{
    public class StockIndexRepository : BaseRepository<StockIndex>, IStockIndexRepository
    {
        public StockIndexRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<string> Get(IEnumerable<string> indices)
        {
            if ( indices is null )
                return null;

            List<string> tickers = new List<string>();
            var fuck = Builders<StockIndex>.Filter.In(x => x.Name, indices);
            foreach(var entry in _dbCollection.FindSync(fuck).ToEnumerable())
            {
                tickers.AddRange(entry.Tickers);
            }

            return tickers;
        }
    }
}

