using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class FreeCashFlowScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_FreeCashFlow()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddFreeCashFlow(1_000_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddFreeCashFlow(1_000_000d));

			AddMarketToScreeningRequest(stockIndex1);
			//AddFreeCashFlowToCustomIndex(10_000_000_000, 10_000_000);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
