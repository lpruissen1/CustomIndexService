using Database.Model.User.CustomIndices;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
    public class PriceToEarningsRatioTTMTest : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToEarningsTTM()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				EarningsPerShare = new List<EarningsPerShare> 
				{ 
					new EarningsPerShare 
					{ 
						earningsPerShare = 1d,
						timestamp = 1561867200
					},
					new EarningsPerShare 
					{ 
						earningsPerShare = 1.2d,
						timestamp = 1569816000
					},
					new EarningsPerShare 
					{ 
						earningsPerShare = 1.07d,
						timestamp = 1577768400
					},
					new EarningsPerShare 
					{ 
						earningsPerShare = 1.1d,
						timestamp = 1585627200
					} 
				} 
			});

			AddDayPriceData(new DayPriceData
			{
				Ticker = ticker1,
				Candle = new List<Candle>
				{
					new Candle
					{
						closePrice = 61.42
					}
				}
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				EarningsPerShare = new List<EarningsPerShare>
				{
					new EarningsPerShare
					{
						earningsPerShare = 0.03d,
						timestamp = 1561867200
					},
					new EarningsPerShare
					{
						earningsPerShare = 0.09d,
						timestamp = 1569816000
					},
					new EarningsPerShare
					{
						earningsPerShare = 0.42d,
						timestamp = 1577768400
					},
					new EarningsPerShare
					{
						earningsPerShare = -0.45d,
						timestamp = 1585627200
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
						closePrice = 143.69
					}
				}
			});

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex1
					}
				},
				PriceToEarningsRatioTTM = new List<PriceToEarningsRatioTTM>()
				{
					new PriceToEarningsRatioTTM {Upper = 25}
                }
			};

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}