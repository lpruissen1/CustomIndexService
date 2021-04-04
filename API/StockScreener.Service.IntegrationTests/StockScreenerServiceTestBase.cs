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

		public void AddSectorToCustomIndex(string sector)
        {
			customIndex.SectorAndIndsutry.Add(new Sector() { Name = sector });
        }

		public void AddIndustryToCustomIndex(string industry)
        {
			customIndex.SectorAndIndsutry.Add(new Sector() { Industries = new[] { industry } });
		}

		public void AddWorkingCapitalToCustomIndex(double upper, double lower)
        {
			customIndex.WorkingCapital.Add(new WorkingCapitals {Upper = upper, Lower = lower});
        }

		public void AddProfitMarginToCustomIndex(double upper, double lower)
        {
			customIndex.ProfitMargin.Add(new ProfitMargins {Upper = upper, Lower = lower});
        }

		public void AddPriceToEarningsRatioToCustomIndex(double upper, double lower)
        {
			customIndex.PriceToEarningsRatioTTM.Add(new PriceToEarningsRatioTTM { Upper = upper, Lower = lower});
        }

		public void AddPayoutRatioToCustomIndex(double upper, double lower)
        {
			customIndex.PayoutRatio.Add(new PayoutRatios {Upper = upper, Lower = lower});
        }

		public void AddMarketCapToCustomIndex(double upper, double lower)
        {
			customIndex.MarketCaps.Add(new MarketCapitalzation { Upper = upper, Lower = lower });
        }

		public void AddGrossMarginToCustomIndex(double upper, double lower)
        {
			customIndex.GrossMargin.Add(new GrossMargins { Upper = upper, Lower = lower});
        }

		public void AddFreeCashFlowToCustomIndex(double upper, double lower)
        {
			customIndex.FreeCashFlow.Add(new FreeCashFlows {Upper = upper, Lower = lower});
        }

		public void AddDividendYieldToCustomIndex(double upper, double lower)
        {
			customIndex.DividendYields.Add(new DividendYield { Upper = upper, Lower = lower});
        }

		public void AddDebtToEquityRatioToCustomIndex(double upper, double lower)
        {
			customIndex.DebtToEquityRatio.Add(new DebtToEquityRatios { Upper = upper, Lower = lower});
        }

		public void AddCurrentRatioToCustomIndex(double upper, double lower)
        {
			customIndex.CurrentRatio.Add(new CurrentRatios { Upper = upper, Lower = lower});
        }

		public void AddRevenueGrowthToCustomIndex(double upper, double lower, int range)
        {
			customIndex.RevenueGrowths.Add(new RevenueGrowth { Upper = upper, Lower = lower, TimePeriod = range});
        }

		public void AddAnnualizedTrailingPerformanceoCustomIndex(double upper, double lower, int range)
        {
			customIndex.TrailingPerformance.Add(new AnnualizedTrailingPerformance { Upper = upper, Lower = lower, TimePeriod = range});
        }
	}
}