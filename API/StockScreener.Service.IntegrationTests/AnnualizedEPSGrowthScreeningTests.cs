using Database.Model.User.CustomIndices;
using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
	[TestFixture]
	public class AnnualizedEPSGrowthScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_AnnualizedEPSGrowth_Biannual()
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
						earningsPerShare = 1.03d,
						timestamp = 1561867200
					},
					new EarningsPerShare
					{
						earningsPerShare = 1.19d,
						timestamp = 1569816000
					},
					new EarningsPerShare
					{
						earningsPerShare = 1.56d,
						timestamp = 1577768400
					},
					new EarningsPerShare
					{
						earningsPerShare = 1.77d,
						timestamp = 1585627200
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
						earningsPerShare = 0.75d,
						timestamp = 1561867200
					},
					new EarningsPerShare
					{
						earningsPerShare = 0.77d,
						timestamp = 1569816000
					},
					new EarningsPerShare
					{
						earningsPerShare = 0.76d,
						timestamp = 1577768400
					},
					new EarningsPerShare
					{
						earningsPerShare = 0.76d,
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
				EPSGrowthAnnualized = new List<EPSGrowthAnnualized>()
				{
					new EPSGrowthAnnualized { Lower = 10, Upper = 130, TimePeriod = 2}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}