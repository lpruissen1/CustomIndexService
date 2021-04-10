using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	[Explicit("Remove custom index datapoint. Does not work")]
	public class WorkingCapitalScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_WorkingCapital()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddWorkingCapital(10_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddWorkingCapital(1_000_000d));

			AddMarketToCustomIndex(stockIndex1);
			//AddWorkingCapitalToCustomIndex(15_000_000, 5_000_000);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}