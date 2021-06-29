using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class InclusionExclusionScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_Inclusion()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "TCKR";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			AddMarketToScreeningRequest(stockIndex1);
			AddIncludedTickerToScreeningRequest(ticker3);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(3, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
			Assert.AreEqual(ticker2, result[1].Ticker);
			Assert.AreEqual(ticker3, result[2].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_Exclusion()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			AddMarketToScreeningRequest(stockIndex1);
			AddExcludedTickerToScreeningRequest(ticker2);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
