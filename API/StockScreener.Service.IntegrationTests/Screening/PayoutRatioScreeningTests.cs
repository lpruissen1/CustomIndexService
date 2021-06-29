using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class PayoutRatioScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_PayoutRatio()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddPayoutRatio(0.1d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddPayoutRatio(0.2d));

			AddMarketToScreeningRequest(stockIndex1);
			//AddPayoutRatioToCustomIndex(0.15, 0);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
