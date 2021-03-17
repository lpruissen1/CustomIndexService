using Database.Core;
using MongoDB.Driver;
using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class StockFinancialsRepository : BaseRepository<StockFinancials>, IStockFinancialsRepository
    {
        private readonly Dictionary<Datapoint, Func<ProjectionDefinition<StockFinancials>>> datapointMapper;

        public StockFinancialsRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<StockFinancials> Get(IEnumerable<string> tickers, IEnumerable<Datapoint> datapoints)
        {
            var projection = Builders<StockFinancials>.Projection.Include(x => x.Ticker).Include(x => x.MarketCap);

            if (datapoints.Contains(Datapoint.MarketCap))
            {
                projection.Include(x => x.MarketCap.Last());
            }
            return _dbCollection.Find(x => tickers.Contains(x.Ticker)).Project<StockFinancials>(projection).ToEnumerable();
        }
    }
}

