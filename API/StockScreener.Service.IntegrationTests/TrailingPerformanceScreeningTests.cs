using Database.Model.User.CustomIndices;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class TrailingPerformanceScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_TrailingPerformance_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddDayPriceData(new DayPriceData 
			{ 
				Ticker = ticker1,
				Candle = new List<Candle>
				{
					new Candle
					{
						timestamp =  1609480830,
						closePrice = 143.69
					},
					new Candle
					{
						timestamp = 1617411630,
						closePrice = 149.20
					}
				}
			});

			AddDayPriceData(new DayPriceData
			{
				Ticker = ticker2,
				Candle = new List<Candle>
				{
					new Candle
					{
						timestamp =  1609480830,
						closePrice = 27.92
					},
					new Candle
					{
						timestamp = 1617411630,
						closePrice = 21.45
					}
				}
			});

			AddMarketToCustomIndex(stockIndex1);
			AddAnnualizedTrailingPerformanceoCustomIndex(100, 0, 1);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}