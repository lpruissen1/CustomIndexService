using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;

namespace StockScreener.Database.Repos
{
	//public class IncomeStatementHistoryRepository : BaseRepository<IncomeStatementHistory>, IIncomeStatementHistoryRepository
	//{
 //       public IncomeStatementHistoryRepository(IMongoDBContext context) : base(context) { }

	//	public override void Update(IncomeStatementHistory info)
	//	{
	//		var filter = Builders<IncomeStatementHistory>.Filter.Eq(e => e.Ticker, info.Ticker);

	//		dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<IncomeStatementHistory, IncomeStatementHistory>() { IsUpsert = true });
	//	}

	//}
}

