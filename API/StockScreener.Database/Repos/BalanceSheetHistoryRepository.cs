using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;

namespace StockScreener.Database.Repos
{
	public class BalanceSheetHistoryRepository : BaseRepository<BalanceSheetHistory>, IBalanceSheetHistoryRepository
	{
        public BalanceSheetHistoryRepository(IMongoDBContext context) : base(context) { }

		public override void Update(BalanceSheetHistory info)
		{
			var filter = Builders<BalanceSheetHistory>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<BalanceSheetHistory, BalanceSheetHistory>() { IsUpsert = true });
		}

	}
}

