using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class RawDividendGrowthScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_DividendGrowthRaw()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.10, 1561867200)
				.AddDividendsPerShare(0.20d, 1569816000)
				.AddDividendsPerShare(0.20d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddRawDividendGrowthToCustomIndex(30, 20, 2);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
