using Core;
using Database.Core;
using Database.Model.User.CustomIndices;
using Database.Repositories;
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
    public abstract class StockScreenerServiceTestBase
	{
		protected IMongoDBContext context;

		protected StockScreenerService sut;

		protected  CustomIndex customIndex;

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

			customIndex = new CustomIndex();
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

		public void AddMarketToCustomIndex(string market)
        {
			customIndex.Markets.Add(market);
        }

		public void AddSectorAndIndustryToCustomIndex(List<string> sector, List<string> industry)
        {
			customIndex.Add(new Sector() { Sectors = sector, Industries = industry });
        }

		public void AddPriceToEarningsRatioToCustomIndex(List<(double Upper, double Lower)> ranges)
        {
			customIndex.Add(new PriceToEarningsRatioTTM { Ranges = ranges.Select(range => new Range { Upper = range.Upper, Lower = range.Lower }).ToList() });
        }

		public void AddDividendYieldToCustomIndex(List<(double Upper, double Lower)> ranges)
		{
			customIndex.Add(new DividendYield { Ranges = ranges.Select(range => new Range { Upper = range.Upper, Lower = range.Lower }).ToList() });
		}

		public void AddPriceToSalesRatioToCustomIndex(List<(double Upper, double Lower)> ranges)
		{
			customIndex.Add(new PriceToSalesRatioTTM { Ranges = ranges.Select(range => new Range { Upper = range.Upper, Lower = range.Lower }).ToList() });
		}

		public void AddMarketCapToCustomIndex(List<(double Upper, double Lower)> ranges)
        {
			customIndex.Add(new MarketCapitalization { Ranges = ranges.Select(range => new Range { Upper = range.Upper, Lower = range.Lower }).ToList() });
        }

		public void AddAnnualizedRevenueGrowthToCustomIndex(List<(double Upper, double Lower, TimePeriod timePeriod)> ranges)
        {
			customIndex.Add(new RevenueGrowthAnnualized { TimedRanges = ranges.Select(range => new TimedRange { Upper = range.Upper, Lower = range.Lower, TimePeriod = range.timePeriod }).ToList() });
        }

		public void AddAnnualizedEPSGrowthToCustomIndex(List<(double Upper, double Lower, TimePeriod timePeriod)> ranges)
        {
			customIndex.Add(new EPSGrowthAnnualized { TimedRanges = ranges.Select(range => new TimedRange { Upper = range.Upper, Lower = range.Lower, TimePeriod = range.timePeriod }).ToList() });
        }

		public void AddAnnualizedTrailingPerformanceoCustomIndex(List<(double Upper, double Lower, TimePeriod timePeriod)> ranges)
        {
			customIndex.Add(new AnnualizedTrailingPerformance { TimedRanges = ranges.Select(range => new TimedRange { Upper = range.Upper, Lower = range.Lower, TimePeriod = range.timePeriod }).ToList() });
        }

		public void AddCoefficientOfVariationToCustomIndex(List<(double Upper, double Lower, TimePeriod timePeriod)> ranges)
		{
			customIndex.Add(new CoefficientOfVariation { TimedRanges = ranges.Select(range => new TimedRange { Upper = range.Upper, Lower = range.Lower, TimePeriod = range.timePeriod }).ToList() });
		}
	}
}