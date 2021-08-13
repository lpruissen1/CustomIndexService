using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
	public class YearEarningsDataRepository : BaseRepository<YearEarningsData>, IYearEarningsDataRepository
    {
        public YearEarningsDataRepository(IMongoDBContext context) : base(context) { }

		public override void Update(YearEarningsData info)
		{
			var filter = Builders<YearEarningsData>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<YearEarningsData, YearEarningsData>() { IsUpsert = true });
		}

		public void Load(IEnumerable<YearEarningsData> info)
		{
			dbCollection.BulkWrite(info.Select(x => new InsertOneModel<YearEarningsData>(x)), new BulkWriteOptions { IsOrdered = true });
		}

	}
	public class YearDividendDataRepository : BaseRepository<YearDividendData>, IYearDividendDataRepository
	{
        public YearDividendDataRepository(IMongoDBContext context) : base(context) { }

		public override void Update(YearDividendData info)
		{
			var filter = Builders<YearDividendData>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<YearDividendData, YearDividendData>() { IsUpsert = true });
		}

		public void Load(IEnumerable<YearDividendData> info)
		{
			dbCollection.BulkWrite(info.Select(x => new InsertOneModel<YearDividendData>(x)), new BulkWriteOptions { IsOrdered = true });
		}

	}
}

