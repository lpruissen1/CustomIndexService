using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Screening
{
	[TestFixture]
	public class MarketCapScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenByStockIndex_MarketCap()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddMarketCap(1_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddMarketCap(10_000d));

			AddMarketToScreeningRequest(stockIndex1);
			AddMarketCapToScreeningRequest(2_000_000d, 200_000d);

			var result = sut.Screen(screeningRequest).Securities;

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
