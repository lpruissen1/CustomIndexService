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

		public IEnumerable<MonthPriceData> GetPirceData(TimePeriod timePeriod)
		{
			throw new NotImplementedException();
		}
	}
}
