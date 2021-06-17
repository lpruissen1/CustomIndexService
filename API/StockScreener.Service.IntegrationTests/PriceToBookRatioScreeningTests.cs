using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	[Explicit("Not Implemented")]
	public class PriceToBookRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToBook()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddBookValuePerShare(14.3d));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(61.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddBookValuePerShare(1.01d));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(39.21));

			AddMarketToScreeningRequest(stockIndex1);
			//AddPriceToBookRatioToCustomIndex(10, 0);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
