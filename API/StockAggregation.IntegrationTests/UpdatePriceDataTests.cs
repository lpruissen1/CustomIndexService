using ApiClient.Models;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation.IntegrationTests
{
	[TestFixture]
	public class UpdatePriceDataTests : AggregationServiceTestBase
	{
		private StockAggregationService sut;

		[Test]
		public void UpdatePriceData_CreatesNewDatabaseEntry_WhenEntryIsLessThanResponse()
		{
			var ticker = "EXMP";

			var open1 = 16.47d;
			var close1 = 16.59d;
			var high1 = 16.68d;
			var low1 = 16.43d;
			var timestamp1 = 1602712080000d;

			var open2 = 16.59d;
			var close2 = 16.63d;
			var high2 = 16.65d;
			var low2 = 16.56d;
			var timestamp2 = 1602714060000d;

			var stubResponse = new PolygonPriceDataResponse()
			{
				Ticker = ticker,
				Results = new List<PolygonCandles>
				{
					new PolygonCandles
					{
						O = open2,
						C = close2,
						H = high2,
						L = low2,
						T = timestamp2
					},
				}
			};

			InsertHourlyPriceData(new PriceData
			{
				Ticker = ticker,
				Candle = new List<Candle>()
				{
					new Candle
					{
						openPrice = open1,
						closePrice = close1,
						highPrice = high1,
						lowPrice = low1,
						timestamp = timestamp1
					}
				},
			});

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateHourlyPriceDataForMarket(market);

			var result = GetPriceData(ticker);

			Assert.AreEqual(2, result.Candle.Count());

			Assert.AreEqual(open1, result.Candle[0].openPrice);
			Assert.AreEqual(close1, result.Candle[0].closePrice);
			Assert.AreEqual(high1, result.Candle[0].highPrice);
			Assert.AreEqual(low1, result.Candle[0].lowPrice);
			Assert.AreEqual(timestamp1, result.Candle[0].timestamp);

			Assert.AreEqual(open2, result.Candle[1].openPrice);
			Assert.AreEqual(close2, result.Candle[1].closePrice);
			Assert.AreEqual(high2, result.Candle[1].highPrice);
			Assert.AreEqual(low2, result.Candle[1].lowPrice);
			Assert.AreEqual(timestamp2, result.Candle[1].timestamp);
		}

		[Test]
		public void UpdatePriceData_DoesNotCreateNewEntry_WhenCollectionIsUpToDate()
		{
			var ticker = "EXMP";

			var open1 = 16.47d;
			var close1 = 16.59d;
			var high1 = 16.68d;
			var low1 = 16.43d;
			var timestamp1 = 1602712080000d;

			var open2 = 16.59d;
			var close2 = 16.63d;
			var high2 = 16.65d;
			var low2 = 16.56d;
			var timestamp2 = 1602712080000d;

			var stubResponse = new PolygonPriceDataResponse()
			{
				Ticker = ticker,
				Results = new List<PolygonCandles>
				{
					new PolygonCandles
					{
						O = open2,
						C = close2,
						H = high2,
						L = low2,
						T = timestamp2
					},
				}
			};

			InsertHourlyPriceData(new PriceData
			{
				Ticker = ticker,
				Candle = new List<Candle>()
				{
					new Candle
					{
						openPrice = open1,
						closePrice = close1,
						highPrice = high1,
						lowPrice = low1,
						timestamp = timestamp1
					}
				},
			});

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateDailyPriceDataForMarket(market);

			var result = GetPriceData(ticker);

			Assert.AreEqual(1, result.Candle.Count());

			Assert.AreEqual(open1, result.Candle[0].openPrice);
			Assert.AreEqual(close1, result.Candle[0].closePrice);
			Assert.AreEqual(high1, result.Candle[0].highPrice);
			Assert.AreEqual(low1, result.Candle[0].lowPrice);
			Assert.AreEqual(timestamp1, result.Candle[0].timestamp);
		}
	}
}
