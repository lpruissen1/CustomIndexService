using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
    public class PriceToEarningsRatioTTMTest : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToEarningsTTM()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddEarningsPerShare(1d, 1561867200)
				.AddEarningsPerShare(1.2d, 1569816000)
				.AddEarningsPerShare(1.07d, 1577768400)
				.AddEarningsPerShare(1.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(61.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddEarningsPerShare(0.03d, 1561867200)
				.AddEarningsPerShare(0.09d, 1569816000)
				.AddEarningsPerShare(0.42d, 1577768400)
				.AddEarningsPerShare(-0.45d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(143.69));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToEarningsRatioToScreeningRequest(25, 0);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
		[Test]
		public void ScreenBy_PriceToEarningsTTM_MissingPriceData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddEarningsPerShare(1d, 1561867200)
				.AddEarningsPerShare(1.2d, 1569816000)
				.AddEarningsPerShare(1.07d, 1577768400)
				.AddEarningsPerShare(1.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(61.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddEarningsPerShare(0.03d, 1561867200)
				.AddEarningsPerShare(0.09d, 1569816000)
				.AddEarningsPerShare(0.42d, 1577768400)
				.AddEarningsPerShare(-0.45d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(143.69));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3)
				.AddEarningsPerShare(0.03d, 1561867200)
				.AddEarningsPerShare(0.09d, 1569816000)
				.AddEarningsPerShare(0.42d, 1577768400)
				.AddEarningsPerShare(-0.45d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToEarningsRatioToScreeningRequest(25, 0);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
		[Test]
		public void ScreenBy_PriceToEarningsTTM_MissingEarningsData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddEarningsPerShare(1d, 1561867200)
				.AddEarningsPerShare(1.2d, 1569816000)
				.AddEarningsPerShare(1.07d, 1577768400)
				.AddEarningsPerShare(1.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(61.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddEarningsPerShare(0.03d, 1561867200)
				.AddEarningsPerShare(0.09d, 1569816000)
				.AddEarningsPerShare(0.42d, 1577768400)
				.AddEarningsPerShare(-0.45d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(143.69));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3)
				.AddEarningsPerShare(0.03d, 1561867200)
				.AddEarningsPerShare(0.09d, 1569816000)
				.AddEarningsPerShare(0.42d, 1577768400)
				.AddEarningsPerShare(-0.45d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToEarningsRatioToScreeningRequest(25, 0);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
