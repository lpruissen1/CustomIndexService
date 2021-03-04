using Database;
using Database.Config;
using Database.Model.StockData;
using Database.Model.User.CustomIndices;
using Database.Repositories;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
    public class StockScreenerTests : StockScreenerServiceTestBase
	{
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ScreenByStockIndexTest()
        {
			var stockIndex1 = "Lee Index";
			var stockIndex2 = "Lee's second Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1 } });
            AddStockIndex(new StockIndex { Name = stockIndex2, Tickers = new[] { ticker2 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2 });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						"Lee Index"
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result.First().Ticker);
        }
    }

	public abstract class StockScreenerServiceTestBase
	{
		protected IMongoDBContext context;

		protected StockScreenerService sut;

		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			var config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\Agg\\Agg\\AggregationService.IntegrationTests").AddJsonFile("appsettings.json").Build();
			var dbSettings = new StockInformationDatabaseSettings() { ConnectionString = config["StockDataDatabaseSettings:ConnectionString"], DatabaseName = config["StockDataDatabaseSettings:DatabaseName"] };

			context = new MongoStockInformationDbContext(dbSettings);
		}

		[OneTimeTearDown]
		public virtual void OneTimeTearDown()
		{

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
	}
}