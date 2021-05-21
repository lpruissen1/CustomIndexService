using ApiClient.Models;
using Core;
using NUnit.Framework;
using System;

namespace StockAggregation.IntegrationTests
{
	[TestFixture]
	public class InsertCompanyInfoTests : AggregationServiceTestBase
	{
		private StockAggregationService sut;

		[Test]
		public void InsertCompanyInfo()
		{
			var ticker = "EXMP";
			var sector = "Energy";
			var industry = "Oil";
			var companyName = "Example";
			var lastUpdated = new DateTime(2001, 1, 1);

			var stubResponse = new PolygonCompanyInfoResponse()
			{
				Industry = industry,
				Sector = sector,
				Name = companyName,
				Updated = lastUpdated
			};

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateCompanyInfoForMarket(market);

			var result = GetCompanyInfo(ticker);

			Assert.AreEqual(industry, result.Industry);
			Assert.AreEqual(sector, result.Sector);
			Assert.AreEqual(companyName, result.Name);
			Assert.AreEqual(lastUpdated.ToUnix(), result.LastUpdated);
		}
	}
}
