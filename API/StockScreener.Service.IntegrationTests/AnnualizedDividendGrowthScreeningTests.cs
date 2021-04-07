using Database.Model.User.CustomIndices;
using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
    public class AnnualizedDividendGrowthScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_DividendGrowthAnnualized_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.10, 1561867200)
				.AddDividendsPerShare(0.20d, 1569816000)
				.AddDividendsPerShare(0.20d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.03d, 1561867200)
				.AddDividendsPerShare(0.03d, 1569816000)
				.AddDividendsPerShare(0.03d, 1577768400)
				.AddDividendsPerShare(0.03d, 1585627200));

			AddMarketToCustomIndex(stockIndex1);
			AddAnnualizedDividendGrowthToCustomIndex(75, 50, 2);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}