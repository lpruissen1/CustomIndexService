using Database.Core;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using StockScreener.Database.Config;
using StockScreener.Database.Model.Price;

namespace StockScreener.Database.IntegrationTests
{
	public class RepoTestBase
	{
		protected IMongoDbContextFactory mongoContextFactory;

		protected IMongoDBContext stockInfoContext;
		protected IMongoDBContext priceContext;

		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			var config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\StockScreener.Database.IntegrationTests").AddJsonFile("appsettings.json").Build();
			stockInfoContext = new MongoStockInformationDbContext(new StockInformationDatabaseSettings() { ConnectionString = config["StockDatabaseSettings:ConnectionString"], DatabaseName = config["StockDatabaseSettings:DatabaseName"] });
			priceContext = new MongoStockInformationDbContext(new StockInformationDatabaseSettings() { ConnectionString = config["PriceDatabaseSettings:ConnectionString"], DatabaseName = config["PriceDatabaseSettings:DatabaseName"] });

			var contextFactory = new Mock<IMongoDbContextFactory>();
			contextFactory.Setup(x => x.GetPriceContext()).Returns(priceContext);
			contextFactory.Setup(x => x.GetStockContext()).Returns(stockInfoContext);

			mongoContextFactory = contextFactory.Object;
		}

		[SetUp]
		protected virtual void SetUp()
		{
			priceContext.ClearAll();
			stockInfoContext.ClearAll();
		}

		public void InserPriceData<TEntry>(TEntry dBEntry) where TEntry : DbEntity
		{
			priceContext.GetCollection<TEntry>(typeof(TEntry).Name).InsertOne(dBEntry);
		}

		public void InsertStockInfoData<TEntry>(TEntry dBEntry) where TEntry : PriceData
		{
			stockInfoContext.GetCollection<TEntry>(typeof(TEntry).Name).InsertOne(dBEntry);
		}
	}
}
