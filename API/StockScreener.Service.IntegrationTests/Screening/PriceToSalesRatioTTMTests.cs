using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
    public class PriceToSalesRatioTTMTests : ScreeningTestBase
	{
		[Test]
		public void Screen_PriceToSalesTTM()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddSalesPerShare(13d, 1561867200)
				.AddSalesPerShare(11.4d, 1569816000)
				.AddSalesPerShare(9.3d, 1577768400)
				.AddSalesPerShare(10.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(165.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddSalesPerShare(1.5d, 1561867200)
				.AddSalesPerShare(1.9d, 1569816000)
				.AddSalesPerShare(1.1d, 1577768400)
				.AddSalesPerShare(1.6d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(303.20));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToSalesRatioToScreeningRequest(10, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
		[Test]
		public void ScreenBy_PriceToSalesTTM_MissingPriceData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddSalesPerShare(13d, 1561867200)
				.AddSalesPerShare(11.4d, 1569816000)
				.AddSalesPerShare(9.3d, 1577768400)
				.AddSalesPerShare(10.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(165.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddSalesPerShare(1.5d, 1561867200)
				.AddSalesPerShare(1.9d, 1569816000)
				.AddSalesPerShare(1.1d, 1577768400)
				.AddSalesPerShare(1.6d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(303.20));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3).AddSalesPerShare(1.5d, 1561867200)
				.AddSalesPerShare(1.9d, 1569816000)
				.AddSalesPerShare(1.1d, 1577768400)
				.AddSalesPerShare(1.6d, 1585627200));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToSalesRatioToScreeningRequest(10, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
		[Test]
		public void ScreenByStockIndex_PriceToSalesTTM_MissingSalesPerShareData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddSalesPerShare(13d, 1561867200)
				.AddSalesPerShare(11.4d, 1569816000)
				.AddSalesPerShare(9.3d, 1577768400)
				.AddSalesPerShare(10.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(165.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddSalesPerShare(1.5d, 1561867200)
				.AddSalesPerShare(1.9d, 1569816000)
				.AddSalesPerShare(1.1d, 1577768400)
				.AddSalesPerShare(1.6d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(303.20));

			InsertData(PriceDataCreator.GetDailyPriceData(ticker3).AddClosePrice(33.20));

			AddMarketToScreeningRequest(stockIndex1);
			AddPriceToSalesRatioToScreeningRequest(10, 1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
