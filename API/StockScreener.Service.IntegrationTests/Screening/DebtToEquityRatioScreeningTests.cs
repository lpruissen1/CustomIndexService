using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class DebtToEquityRatioScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_DebtToEquityRatio()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddDebtToEquityRatio(0.75d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddDebtToEquityRatio(2.5d));

			AddMarketToScreeningRequest(stockIndex1);
			//AddDebtToEquityRatioToCustomIndex(1, 0);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
