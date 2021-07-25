using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;

namespace StockScreener.Database.Repos
{
	public class OutstandingSharesRepository : BaseRepository<OutstandingSharesHistory>, IOutstandingSharesRepository
    {
        public OutstandingSharesRepository(IMongoDBContext context) : base(context) { }

		public override void Update(OutstandingSharesHistory info)
		{
			var filter = Builders<OutstandingSharesHistory>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<OutstandingSharesHistory, OutstandingSharesHistory>() { IsUpsert = true });
		}

	}
}

