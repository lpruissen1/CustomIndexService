using Database.Core;

namespace StockScreener.Database
{
	public interface IMongoDbContextFactory
	{
		IMongoDBContext GetPriceContext();
		IMongoDBContext GetStockContext();
	}
}
