using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;

namespace StockScreener.Database.Repos
{
	public class CashFlowHistoryRepository : BaseRepository<CashFlowHistory>, ICashFlowHistoryRepository
	{
        public CashFlowHistoryRepository(IMongoDBContext context) : base(context) { }

		public override void Update(CashFlowHistory info)
		{
			var filter = Builders<CashFlowHistory>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<CashFlowHistory, CashFlowHistory>() { IsUpsert = true });
		}

	}
}

