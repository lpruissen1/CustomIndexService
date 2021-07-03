using Core;
using NUnit.Framework;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class CoefficientOfVariationScreeningTests : ScreeningTestBase
	{
		[Test]
		public void ScreenBy_CoefficientOfVariation_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(67.54, 1609480830)
				.AddClosePrice(68.65, 1610411630)
				.AddClosePrice(70.01, 1611411630)
				.AddClosePrice(69.69, 1612411630)
				.AddClosePrice(67.12, 1613411630)
				.AddClosePrice(62.29, 1614411630)
				.AddClosePrice(64.13, 1615411630)
				.AddClosePrice(64.45, 1617411630));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(123.12, 1609480830)
				.AddClosePrice(129.45, 1610411630)
				.AddClosePrice(130.01, 1611411630)
				.AddClosePrice(139.67, 1612411630)
				.AddClosePrice(125.16, 1613411630)
				.AddClosePrice(112.63, 1614411630)
				.AddClosePrice(107.40, 1615411630)
				.AddClosePrice(118.79, 1617411630));

			AddMarketToScreeningRequest(stockIndex1);
			AddCoefficientOfVariationToScreeningRequest(4.3, 0, TimePeriod.Quarter);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenBy_CoefficientOfVariation_MissingPriceData()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			InsertData(StockIndexCreator.GetStockIndex(stockIndex1).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(67.54, 1609480830)
				.AddClosePrice(68.65, 1610411630)
				.AddClosePrice(70.01, 1611411630)
				.AddClosePrice(69.69, 1612411630)
				.AddClosePrice(67.12, 1613411630)
				.AddClosePrice(62.29, 1614411630)
				.AddClosePrice(64.13, 1615411630)
				.AddClosePrice(64.45, 1617411630));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(123.12, 1609480830)
				.AddClosePrice(129.45, 1610411630)
				.AddClosePrice(130.01, 1611411630)
				.AddClosePrice(139.67, 1612411630)
				.AddClosePrice(125.16, 1613411630)
				.AddClosePrice(112.63, 1614411630)
				.AddClosePrice(107.40, 1615411630)
				.AddClosePrice(118.79, 1617411630));

			AddMarketToScreeningRequest(stockIndex1);
			AddCoefficientOfVariationToScreeningRequest(4.3, 0, TimePeriod.Quarter);

			var result = sut.Screen(screeningRequest);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}
