using Database.Model.User.CustomIndices;
using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
    public class DividendYieldScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_DividendYield()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				DividendsPerShare = new List<DividendsPerShare> 
				{ 
					new DividendsPerShare 
					{ 
						dividendsPerShare = 0.25d,
						timestamp = 1561867200
					},
					new DividendsPerShare 
					{ 
						dividendsPerShare = 0.25d,
						timestamp = 1569816000
					},
					new DividendsPerShare 
					{ 
						dividendsPerShare = 0.25d,
						timestamp = 1577768400
					},
					new DividendsPerShare 
					{ 
						dividendsPerShare = 0.25d,
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
						closePrice = 50.02
					}
				}
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				DividendsPerShare = new List<DividendsPerShare>
				{
					new DividendsPerShare
					{
						dividendsPerShare = 0.03d,
						timestamp = 1561867200
					},
					new DividendsPerShare
					{
						dividendsPerShare = 0.03d,
						timestamp = 1569816000
					},
					new DividendsPerShare
					{
						dividendsPerShare = 0.03d,
						timestamp = 1577768400
					},
					new DividendsPerShare
					{
						dividendsPerShare = 0.03d,
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
						closePrice = 80.01
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
				DividendYield = new List<DividendYield>()
				{
					new DividendYield {Lower = 1, Upper = 5}
                }
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}