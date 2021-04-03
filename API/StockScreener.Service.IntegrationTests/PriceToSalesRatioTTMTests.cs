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
    public class PriceToSalesRatioTTMTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToSalesTTM()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				SalesPerShare = new List<SalesPerShare> 
				{ 
					new SalesPerShare 
					{ 
						salesPerShare = 13d,
						timestamp = 1561867200
					},
					new SalesPerShare 
					{ 
						salesPerShare = 11.4d,
						timestamp = 1569816000
					},
					new SalesPerShare 
					{ 
						salesPerShare = 9.3d,
						timestamp = 1577768400
					},
					new SalesPerShare 
					{ 
						salesPerShare = 10.1d,
						timestamp = 1585627200
					} 
				} 
			});

			AddDailyPriceData(new DayPriceData
			{
				Ticker = ticker1,
				Candle = new List<Candle>
				{
					new Candle
					{
						closePrice = 165.42
					}
				}
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				SalesPerShare = new List<SalesPerShare>
				{
					new SalesPerShare
					{
						salesPerShare = 1.5d,
						timestamp = 1561867200
					},
					new SalesPerShare
					{
						salesPerShare = 1.9d,
						timestamp = 1569816000
					},
					new SalesPerShare
					{
						salesPerShare = 1.1d,
						timestamp = 1577768400
					},
					new SalesPerShare
					{
						salesPerShare = 1.6d,
						timestamp = 1585627200
					}
				}
			});

			AddDailyPriceData(new DayPriceData
			{
				Ticker = ticker2,
				Candle = new List<Candle>
				{
					new Candle
					{
						closePrice = 303.20
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
				PriceToSalesRatioTTM = new List<PriceToSalesRatioTTM>()
				{
					new PriceToSalesRatioTTM {Lower = 1, Upper = 10}
                }
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}