//using Database.Model.User.CustomIndices;
using Database.Model.User.CustomIndices;
using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;
using CustomIndexMarketCap = Database.Model.User.CustomIndices.MarketCap;
using MarketCap = StockScreener.Database.Model.StockFinancials.MarketCap;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class MarketCapScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_MarketCap()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				MarketCap = new List<MarketCap> 
				{ 
					new MarketCap 
					{ 
						marketCap = 1_000_000d 
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				MarketCap = new List<MarketCap>
				{
					new MarketCap
					{
						marketCap = 10_000d
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
				MarketCaps = new MarketCaps()
                {
				   MarketCapGroups = new[] { new CustomIndexMarketCap { Lower = 200_000d, Upper = 2_000_000d } }
                }
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}