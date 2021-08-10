using Core;
using Core.Extensions;
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
	public class QuarterPriceDataRepository : BaseRepository<QuarterPriceData>, IQuarterhPriceDataRepository
	{
		public QuarterPriceDataRepository(IMongoDBContext context) : base(context)	{ }

		public QuarterPriceDataRepository(IMongoDbContextFactory contextFactory) : base(contextFactory.GetPriceContext()) { }

		public void LoadPriceData(IEnumerable<QuarterPriceData> priceData)
		{
			dbCollection.BulkWriteAsync(priceData.Select(x => new InsertOneModel<QuarterPriceData>(x)), new BulkWriteOptions { IsOrdered = true});
		}

		public void UpdatePriceData(QuarterPriceData priceData)
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, IEnumerable<QuarterPriceData>> GetPriceData(IEnumerable<string> tickers, TimePeriod timePeriod)
		{
			var result = new Dictionary<string, IEnumerable<QuarterPriceData>>();

			var tickerFilter = Builders<QuarterPriceData>.Filter.In(e => e.Ticker, tickers);

			var timeRange = DateTime.Now.AddMonths((timePeriod.GetMonthsFromTimePeriod() + 3) * -1);
			var timeRangeFilter = Builders<QuarterPriceData>.Filter.Where(e => e.Month >= timeRange);

			var combinedFilter = Builders<QuarterPriceData>.Filter.And(tickerFilter, timeRangeFilter);
			
			var data = dbCollection.Find(combinedFilter).ToList();
			
			foreach(var ticker in tickers) 
			{
				result.Add(ticker, data.Where(x => x.Ticker == ticker));
			}

			return result;
		}
	}
}
