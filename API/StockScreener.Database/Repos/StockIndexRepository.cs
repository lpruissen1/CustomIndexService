using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
    public class StockIndexRepository : BaseRepository<StockIndex>, IStockIndexRepository
    {
        public StockIndexRepository(IMongoDBContext context) : base(context) { }

		public void CreateEntryForExchange(string name, List<string> tickers)
		{
			var item = new StockIndex()
			{
				Name = name,
				Tickers = tickers
			};

			dbCollection.InsertOne(item);
		}

		public IEnumerable<string> Get(IEnumerable<string> indices)
        {
            if ( indices is null )
                return null;

            List<string> tickers = new List<string>();
            var filter = Builders<StockIndex>.Filter.In(x => x.Name, indices);
            foreach ( var entry in dbCollection.FindSync(filter).ToEnumerable() )
            {
                tickers.AddRange(entry.Tickers);
            }

            return tickers;
        }

        public StockIndex GetIndex(string name)
        {
            return dbCollection.Find(index => index.Name == name).FirstOrDefault();
        }
    }
}

