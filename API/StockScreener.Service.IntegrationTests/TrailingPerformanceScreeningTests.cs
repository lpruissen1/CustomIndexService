using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class TrailingPerformanceScreeningTests : StockScreenerServiceTestBase
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

			AddMarketToCustomIndex(stockIndex1);
			AddAnnualizedTrailingPerformanceoCustomIndex(100, 0, 1);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}