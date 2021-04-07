using Database.Core;
using Database.Repositories;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;

namespace StockScreener.Service.IntegrationTests.Weighting
{
	[TestFixture]
	public class WeightingFatPeopleTestBase
	{
		protected IMongoDBContext context;
	
		protected StockScreenerService sut;
		
		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			var config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\StockScreener.Service.IntegrationTests").AddJsonFile("appsettings.json").Build();
			var dbSettings = new StockInformationDatabaseSettings() { ConnectionString = config["StockDatabaseSettings:ConnectionString"], DatabaseName = config["StockDatabaseSettings:DatabaseName"] };
	
			context = new MongoStockInformationDbContext(dbSettings);
		}
	
		[SetUp]
		public virtual void SetUp()
		{
			context.ClearAll();
	
			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));
		}

		public void InsertData<TEntry>(TEntry dBEntry)
		{
			context.GetCollection<TEntry>(typeof(TEntry).Name).InsertOne(dBEntry);
		}

	}
}
