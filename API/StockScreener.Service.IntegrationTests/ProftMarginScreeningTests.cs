using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class ProfitMarginScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_ProfitMargin()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddProfitMargin(0.4d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddProfitMargin(0.05d));

			AddMarketToScreeningRequest(stockIndex1);
			//AddProfitMarginToCustomIndex(.5, .1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
