using Database.Core;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
	public class StockScreenerServiceTestBase
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

		public void AddStockIndex(string indexName, IEnumerable<string> stockIndex)
		{
			context.GetCollection<StockIndex>("StockIndex").InsertOne(new StockIndex { Name = indexName, Tickers = stockIndex.ToList() });
		}

		public void InsertData<TEntry>(TEntry dBEntry)
		{
			context.GetCollection<TEntry>(typeof(TEntry).Name).InsertOne(dBEntry);
		}
	}
}
