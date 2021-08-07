using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	[Explicit("Not Implemented")]
	public class AnnualizedDividendGrowthScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.20d, 1577768400)
				.AddDividendsPerShare(0.22, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToScreeningRequest(50, 20, 1);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_Biannual()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.20d, 1569816000)
				.AddDividendsPerShare(0.25, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToScreeningRequest(75, 50, 2);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_Yearly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.18d, 1554004800)
				.AddDividendsPerShare(0.25d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.75d, 1554004800)
				.AddDividendsPerShare(0.79d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToCustomIndex(40, 25, 4);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_ThreeYear()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.15d, 1490954000)
				.AddDividendsPerShare(0.25d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.75d, 1490954000)
				.AddDividendsPerShare(0.79d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToCustomIndex(20, 10, 12);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_FiveYear()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.15d, 1427839200)
				.AddDividendsPerShare(0.25d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.75d, 1427839200)
				.AddDividendsPerShare(0.79d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToCustomIndex(11, 10, 20);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_MultipleRanges()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "TEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.18d, 1554004800)
				.AddDividendsPerShare(0.25d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.75d, 1554004800)
				.AddDividendsPerShare(0.79d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3)
				.AddDividendsPerShare(0.10d, 1554004800)
				.AddDividendsPerShare(0.30d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToCustomIndex(40, 25, 4);
			//AddAnnualizedDividendGrowthToCustomIndex(300, 150, 4);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
			Assert.AreEqual(ticker3, result[1].Ticker);
		}

		// Test fails because screener adds tickers that fit either of the two ranges, not only those that fit both
		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_MultipleTimePeriods()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "TEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.15d, 1490954000)
				.AddDividendsPerShare(0.18d, 1554004800)
				.AddDividendsPerShare(0.25d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.74d, 1490954000)
				.AddDividendsPerShare(0.75d, 1554004800)
				.AddDividendsPerShare(0.79d, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3)
				.AddDividendsPerShare(0.11d, 1490954000)
				.AddDividendsPerShare(0.10d, 1554004800)
				.AddDividendsPerShare(0.14d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			//AddAnnualizedDividendGrowthToCustomIndex(45, 30, 4);
			//AddAnnualizedDividendGrowthToCustomIndex(20, 16, 12);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
