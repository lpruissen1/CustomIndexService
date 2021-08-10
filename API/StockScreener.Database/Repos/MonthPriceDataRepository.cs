using Core;
using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
	public class MonthPriceDataRepository : BaseRepository<MonthPriceData>, IMonthPriceDataRepository
	{
		public MonthPriceDataRepository(IMongoDBContext context) : base(context)	{ }

		public MonthPriceDataRepository(IMongoDbContextFactory contextFactory) : base(contextFactory.GetPriceContext()) { }

		public void LoadPriceData(List<MonthPriceData> priceData)
		{
			dbCollection.BulkWriteAsync(priceData.Select(x => new InsertOneModel<MonthPriceData>(x)), new BulkWriteOptions { IsOrdered = true});
		}

		public void UpdatePriceData(MonthPriceData priceData)
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, IEnumerable<MonthPriceData>> GetPriceData(IEnumerable<string> tickers, TimePeriod timePeriod)
		{
			var result = new Dictionary<string, IEnumerable<MonthPriceData>>();
			var filter = Builders<MonthPriceData>.Filter.In(e => e.Ticker, tickers);
			var timeFilter = Builders<MonthPriceData>.Filter.Where(e => e.Month >= DateTime.Now.FromTimePeriod(timePeriod));

			var combinedFilter = Builders<MonthPriceData>.Filter.And(filter, timeFilter);
			var data = dbCollection.Find(combinedFilter).ToList();
			
			foreach(var ticker in tickers) 
			{
				result.Add(ticker, data.Where(x => x.Ticker == ticker));
			}

			return result;
		}
	}
}
