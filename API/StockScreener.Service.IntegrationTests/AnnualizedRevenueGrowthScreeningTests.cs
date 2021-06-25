using Core;
using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class AnnualizedRevenueGrowthScreeningTests : StockScreenerServiceTestBase
	{

		[Test]
		public void ScreenByStockIndex_RevenueGrowth_Biannual()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(1_500_000d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(-400_000d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddAnnualizedRevenueGrowthToScreeningRequest(301, 299, TimePeriod.HalfYear);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_RevenueGrowth_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(1_500_000d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(400_000d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddAnnualizedRevenueGrowthToScreeningRequest(407, 404, TimePeriod.Quarter);


			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_RevenueGrowth_MissingRevenueData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(1_500_000d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddRevenue(500_000d, 1561867200)
				.AddRevenue(750_000d, 1569816000)
				.AddRevenue(1_000_000d, 1577768400)
				.AddRevenue(400_000d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddAnnualizedRevenueGrowthToScreeningRequest(407, 404, TimePeriod.Quarter);


			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
