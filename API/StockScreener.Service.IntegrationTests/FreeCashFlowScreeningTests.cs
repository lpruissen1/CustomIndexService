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
	public class FreeCashFlowScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_FreeCashFlow()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				FreeCashFlow = new List<FreeCashFlow> 
				{ 
					new FreeCashFlow 
					{ 
						freeCashFlow = 1_000_000_000d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				FreeCashFlow = new List<FreeCashFlow>
				{
					new FreeCashFlow
					{
						freeCashFlow = 1_000_000d
					}
				}
			}) ;

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex1
					}
				},
				FreeCashFlow = new List<FreeCashFlows>()
				{
					new FreeCashFlows {Lower = 10_000_000, Upper = 10_000_000_000}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}