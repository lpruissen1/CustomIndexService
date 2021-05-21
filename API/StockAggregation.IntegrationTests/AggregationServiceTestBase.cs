using Database.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NUnit.Framework;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockAggregation.IntegrationTests
{
	public abstract class AggregationServiceTestBase
	{
		protected IMongoDBContext stockContext;
		protected IMongoDBContext priceContext;

		protected const string market = "ExampleMarket";

		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			var config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\StockAggregation.IntegrationTests").AddJsonFile("appsettings.json").Build();
			var stockDBSettings = new StockInformationDatabaseSettings() { ConnectionString = config["StockDatabaseSettings:ConnectionString"], DatabaseName = config["StockDatabaseSettings:DatabaseName"] };
			var priceDBSettings = new StockInformationDatabaseSettings() { ConnectionString = config["PriceDatabaseSettings:ConnectionString"], DatabaseName = config["PriceDatabaseSettings:DatabaseName"] };

			stockContext = new MongoStockInformationDbContext(stockDBSettings);
			stockContext = new MongoStockInformationDbContext(stockDBSettings);
			priceContext = new MongoStockInformationDbContext(priceDBSettings);
		}

		[OneTimeTearDown]
		public virtual void OneTimeTearDown()
		{

		}

		[SetUp]
		public virtual void SetUp()
		{
			InsertStockIndexData(new StockIndex() { Name = market, Tickers = new List<string> { "EXMP" } });
		}

		[TearDown]
		public virtual void TearDown()
		{
			stockContext.ClearAll();
			priceContext.ClearAll();
		}

		protected StockFinancials GetStockFinancials(string ticker)
		{
			return stockContext.GetCollection<StockFinancials>("StockFinancials").Find(x => x.Ticker == ticker).FirstOrDefault();
		}

		protected void InsertStockFinancials(StockFinancials stockFinancials)
		{
			stockContext.GetCollection<StockFinancials>("StockFinancials").InsertOne(stockFinancials);
		}

		protected CompanyInfo GetCompanyInfo(string ticker)
		{
			return stockContext.GetCollection<CompanyInfo>("CompanyInfo").Find(x => x.Ticker == ticker).FirstOrDefault();
		}

		protected void InsertCompanyInfo(CompanyInfo companyInfo)
		{
			stockContext.GetCollection<CompanyInfo>("CompanyInfo").InsertOne(companyInfo);
		}

		protected PriceData GetPriceData(string ticker)
		{
			return priceContext.GetCollection<HourPriceData>("HourPriceData").Find(x => x.Ticker == ticker).FirstOrDefault();
		}

		protected void InsertHourlyPriceData(PriceData priceData)
		{
			priceContext.GetCollection<PriceData>("HourPriceData").InsertOne(priceData);
		}

		protected void InsertStockIndexData(StockIndex index)
		{
			stockContext.GetCollection<StockIndex>("StockIndex").InsertOne(index);
		}
	}
}
