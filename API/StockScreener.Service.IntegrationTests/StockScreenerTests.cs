using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
    public class StockScreenerTests : StockScreenerServiceTestBase
	{
        [Test]
        public void ScreenByStockIndex_SingleIndexTest()
        {
			var stockIndex1 = "Lee's Index";
			var stockIndex2 = "Lee's second Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockIndexCreator.GetStockIndex(stockIndex2).AddTicker(ticker3));

			AddMarketToScreeningRequest(stockIndex1);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result.First().Ticker);
			Assert.AreEqual(ticker2, result.Last().Ticker);
        }

        [Test]
        public void ScreenByStockIndex_MultipleIndicesTest()
        {
			var stockIndex1 = "Lee's Index";
			var stockIndex2 = "Lee's second Index";
			var stockIndex3 = "Lee's third Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1));
			InsertData(StockIndexCreator.GetStockIndex(stockIndex2).AddTicker(ticker2));
			InsertData(StockIndexCreator.GetStockIndex(stockIndex3).AddTicker(ticker3));

			AddMarketToScreeningRequest(stockIndex1);
			AddMarketToScreeningRequest(stockIndex3);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result.First().Ticker);
			Assert.AreEqual(ticker3, result.Last().Ticker);
        }
    }
}
