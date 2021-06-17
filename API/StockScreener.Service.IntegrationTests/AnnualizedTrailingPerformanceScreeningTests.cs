using Core;
using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class AnnualizedTrailingPerformanceScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_TrailingPerformance_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(143.69, 1609480830).AddClosePrice(149.20, 1617411630));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(27.92, 1609480830).AddClosePrice(21.45, 1617411630));

			AddMarketToScreeningRequest(stockIndex1);
			AddAnnualizedTrailingPerformanceoScreeningRequest(100, 0, TimePeriod.Quarter);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
