using Core;
using Database.Core;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StockScreener.Core.Request;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
    public abstract class StockScreenerServiceTestBase
	{
		protected IMongoDBContext context;

		protected StockScreenerService sut;

		protected ScreeningRequest screeningRequest;

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

			screeningRequest = new ScreeningRequest();
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

		public void AddMarketToScreeningRequest(string market)
        {
			screeningRequest.Markets.Add(market);
        }

		public void AddIncludedTickerToScreeningRequest(string ticker)
        {
			screeningRequest.Inclusions.Add(ticker);
        }

		public void AddExcludedTickerToScreeningRequest(string ticker)
        {
			screeningRequest.Exclusions.Add(ticker);
        }

		public void AddSectorAndIndustryToScreeningRequest(List<string> sector, List<string> industry)
        {
			screeningRequest.Industries = industry;
			screeningRequest.Sectors = sector;
        }

		public void AddPriceToEarningsRatioToScreeningRequest(double upper, double lower)
        {
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.PriceToEarningsRatioTTM });
        }

		public void AddDividendYieldToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.DividendYield });
		}

		public void AddPriceToSalesRatioToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.PriceToSalesRatioTTM });
		}

		public void AddMarketCapToScreeningRequest(double upper, double lower)
        {
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.MarketCap });
        }

		public void AddAnnualizedRevenueGrowthToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
        {
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.RevenueGrowthAnnualized });
        }

		public void AddAnnualizedEPSGrowthToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
        {
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.EPSGrowthAnnualized });
        }

		public void AddAnnualizedTrailingPerformanceoScreeningRequest(double upper, double lower, TimePeriod timePeriod)
        {
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.AnnualizedTrailingPerformance });
        }

		public void AddCoefficientOfVariationToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
		{
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.CoefficientOfVariation });
		}
	}
}
