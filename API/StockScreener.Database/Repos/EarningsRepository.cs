using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;

namespace StockScreener.Database.Repos
{
	public class EarningsRepository : BaseRepository<EarningsHistory>, IEarningsRepository
    {
        public EarningsRepository(IMongoDBContext context) : base(context) { }

		public override void Update(EarningsHistory info)
		{
			var filter = Builders<EarningsHistory>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<EarningsHistory, EarningsHistory>() { IsUpsert = true });
		}

	}
}

