using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
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
            foreach ( var entry in dbCollection.FindSync(fuck).ToEnumerable() )
            {
                tickers.AddRange(entry.Tickers);
            }

            return tickers;
        }
    }
}

