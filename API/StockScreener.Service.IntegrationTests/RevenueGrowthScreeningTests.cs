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
		public void ScreenByStockIndex_RevenueGrowth_Biannual()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				Revenues = new List<Revenues> 
				{ 
					new Revenues 
					{ 
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues 
					{ 
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues 
					{ 
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues 
					{ 
						revenues = 1_500_000d,
						timestamp = 1585627200
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				Revenues = new List<Revenues>
				{
					new Revenues
					{
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues
					{
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues
					{
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues
					{
						revenues = 400_000d,
						timestamp = 1585627200
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
				RevenueGrowths = new List<RevenueGrowth>()
				{
					new RevenueGrowth { Lower = 299, Upper = 301, TimePeriod = 2}
                }
			};

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

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

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex1
					}
				},
				TrailingPerformance = new List<AnnualizedTrailingPerformance>()
				{
					new AnnualizedTrailingPerformance { Lower = 0, Upper = 100, TimePeriod = 1}
                }
			};

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}

	[TestFixture]
	public class RevenueGrowthScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_RevenueGrowth_Biannual()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				Revenues = new List<Revenues> 
				{ 
					new Revenues 
					{ 
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues 
					{ 
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues 
					{ 
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues 
					{ 
						revenues = 1_500_000d,
						timestamp = 1585627200
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				Revenues = new List<Revenues>
				{
					new Revenues
					{
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues
					{
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues
					{
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues
					{
						revenues = 400_000d,
						timestamp = 1585627200
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
				RevenueGrowths = new List<RevenueGrowth>()
				{
					new RevenueGrowth { Lower = 299, Upper = 301, TimePeriod = 2}
                }
			};

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}

		[Test]
		public void ScreenByStockIndex_RevenueGrowth_Quarterly()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				Revenues = new List<Revenues> 
				{ 
					new Revenues 
					{ 
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues 
					{ 
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues 
					{ 
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues 
					{ 
						revenues = 1_500_000d,
						timestamp = 1585627200
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				Revenues = new List<Revenues>
				{
					new Revenues
					{
						revenues = 500_000d,
						timestamp = 1561867200
					},
					new Revenues
					{
						revenues = 750_000d,
						timestamp = 1569816000
					},
					new Revenues
					{
						revenues = 1_000_000d,
						timestamp = 1577768400
					},
					new Revenues
					{
						revenues = 400_000d,
						timestamp = 1585627200
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
				RevenueGrowths = new List<RevenueGrowth>()
				{
					new RevenueGrowth { Lower = 404, Upper = 407, TimePeriod = 1}
                }
			};

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}