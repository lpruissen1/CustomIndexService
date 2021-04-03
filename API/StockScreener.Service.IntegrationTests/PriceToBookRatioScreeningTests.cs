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
    public class PriceToBookRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PriceToBook()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				BookValuePerShare = new List<BookValuePerShare> 
				{ 
					new BookValuePerShare 
					{ 
						bookValuePerShare = 14.3d,
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
						closePrice = 61.42
					}
				}
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				BookValuePerShare = new List<BookValuePerShare>
				{
					new BookValuePerShare
					{
						bookValuePerShare = 1.01,
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
						closePrice = 39.21d
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
				PriceToBookValue = new List<PriceToBookValue>()
				{
					new PriceToBookValue {Upper = 10}
                }
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}