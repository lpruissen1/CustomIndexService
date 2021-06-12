using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class CurrentRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_CurrentRatio()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddCurrentRatio(3.1d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddCurrentRatio(0.75d));

			AddMarketToScreeningRequest(stockIndex1);
			//AddCurrentRatioToCustomIndex(4, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
