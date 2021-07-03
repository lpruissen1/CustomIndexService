using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
    public class DividendYieldScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_DividendYield()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(50.02));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(80.01));

			AddMarketToScreeningRequest(stockIndex1);
			AddDividendYieldToScreeningRequest(5, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendYieldMissing()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(50.02));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(80.01));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddDividendYieldToScreeningRequest(5, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_PriceDataMissing()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(50.02));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(80.01));

			InsertData(PriceDataCreator.GetDailyPriceData(ticker3).AddClosePrice(30.01));

			AddMarketToScreeningRequest(stockIndex1);
			AddDividendYieldToScreeningRequest(5, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
