using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
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

			AddMarketToCustomIndex(stockIndex1);
			AddAnnualizedRevenueGrowthToCustomIndex(301, 299, 2);

			var result = sut.Screen(customIndex);

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

			AddMarketToCustomIndex(stockIndex1);
			AddAnnualizedRevenueGrowthToCustomIndex(407, 404, 1);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}