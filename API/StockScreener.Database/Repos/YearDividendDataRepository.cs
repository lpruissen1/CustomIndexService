using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
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

