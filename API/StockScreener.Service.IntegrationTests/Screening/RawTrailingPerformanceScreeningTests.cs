﻿using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class RawTrailingPerformanceScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_RawTrailingPerformance_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(143.69, 1609480830).AddClosePrice(149.20, 1617411630));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(27.92, 1609480830).AddClosePrice(21.45, 1617411630));

			AddMarketToScreeningRequest(stockIndex1);
			//AddRawTrailingPerformanceToCustomIndex(25, 0, 1);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
