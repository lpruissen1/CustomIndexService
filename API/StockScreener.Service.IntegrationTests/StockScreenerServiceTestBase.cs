using Database.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System;
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

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new MonthPriceDataRepository(context)), new FakeLogger());
		}

		public void AddStockIndex(string indexName, IEnumerable<string> stockIndex)
		{
			context.GetCollection<StockIndex>("StockIndex").InsertOne(new StockIndex { Name = indexName, Tickers = stockIndex.ToList() });
		}

		public void InsertData<TEntry>(TEntry dBEntry)
		{
			context.GetCollection<TEntry>(typeof(TEntry).Name).InsertOne(dBEntry);
		}

		public class FakeLogger : ILogger
		{
			public IDisposable BeginScope<TState>(TState state)
			{
				throw new NotImplementedException();
			}

			public bool IsEnabled(LogLevel logLevel)
			{
				throw new NotImplementedException();
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
			{
				
			}
		}
	}
}
