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
	[Explicit("Remove custom index datapoint. Does not work")]
	public class PriceToBookRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToBook()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddBookValuePerShare(14.3d));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(61.42));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddBookValuePerShare(1.01d));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(39.21));

			AddMarketToCustomIndex(stockIndex1);
			//AddPriceToBookRatioToCustomIndex(10, 0);

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}