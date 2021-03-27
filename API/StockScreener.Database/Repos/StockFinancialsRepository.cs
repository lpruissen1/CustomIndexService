﻿using Database.Core;
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

        private StockFinancialsProjectionBuilder projectionBuilder = new StockFinancialsProjectionBuilder();
        public StockFinancialsRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<StockFinancials> Get(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
            var projection = Builders<StockFinancials>.Projection.Include(x => x.Ticker).Include(x => x.MarketCap);

            if (datapoints.Contains(BaseDatapoint.MarketCap))
            {
                projection.Include(x => x.MarketCap.Last());
            }

            return dbCollection.Find(x => tickers.Contains(x.Ticker)).Project<StockFinancials>(projection).ToEnumerable();
        }

        public StockFinancials Get(string ticker, IEnumerable<BaseDatapoint> datapoints)
        {
            var projection = projectionBuilder.BuildProjection(datapoints);

            return dbCollection.Find(x => ticker == x.Ticker).Project<StockFinancials>(projection).FirstOrDefault();
        }
    }
}