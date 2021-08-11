using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
	public class YearCashFlowDataRepository : BaseRepository<YearCashFlowData>, IYearCashFlowDataRepository
	{
        public YearCashFlowDataRepository(IMongoDBContext context) : base(context) { }

		public void Load(IEnumerable<YearCashFlowData> data)
		{
			dbCollection.BulkWrite(data.Select(x => new InsertOneModel<YearCashFlowData>(x)), new BulkWriteOptions { IsOrdered = true });
		}

		public override void Update(YearCashFlowData info)
		{
			var filter = Builders<YearCashFlowData>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<YearCashFlowData, YearCashFlowData>() { IsUpsert = true });
		}
	}
}

