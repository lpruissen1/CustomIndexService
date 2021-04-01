using Database;
using Database.Core;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StockScreener.Database.Config;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;

namespace StockScreener.Service.IntegrationTests
{
    public abstract class StockScreenerServiceTestBase
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

		[OneTimeTearDown]
		public virtual void OneTimeTearDown()
		{
			context.ClearAll();
		}

		[TearDown]
		public virtual void TearDown()
		{
			context.ClearAll();
		}

		public void AddStockIndex(StockIndex stockIndex)
        {
			context.GetCollection<StockIndex>("StockIndex").InsertOne(stockIndex);
        }

		public void AddCompanyInfo(CompanyInfo companyInfo)
        {
			context.GetCollection<CompanyInfo>("CompanyInfo").InsertOne(companyInfo);
        }

		public void AddStockFinancials(StockFinancials stockFinancials)
        {
			context.GetCollection<StockFinancials>("StockFinancials").InsertOne(stockFinancials);
        }

		public void AddDailyPriceData(DayPriceData dayPriceData)
		{
			context.GetCollection<DayPriceData>("DayPriceData").InsertOne(dayPriceData);
		}
	}
}