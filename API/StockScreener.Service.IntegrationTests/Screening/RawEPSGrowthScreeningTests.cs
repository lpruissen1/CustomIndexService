using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
    [TestFixture]
	public class RawEPSGrowthScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_RawEPSGrowth_Biannual()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddEarningsPerShare(1.03d, 1561867200)
				.AddEarningsPerShare(1.19d, 1569816000)
				.AddEarningsPerShare(1.56d, 1577768400)
				.AddEarningsPerShare(1.77d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddEarningsPerShare(0.75d, 1561867200)
				.AddEarningsPerShare(1.77d, 1569816000)
				.AddEarningsPerShare(1.76d, 1577768400)
				.AddEarningsPerShare(1.76d, 1585627200));

			AddMarketToCustomIndex(stockIndex1);
			AddRawEPSGrowthToCustomIndex(50, 10, 2);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}