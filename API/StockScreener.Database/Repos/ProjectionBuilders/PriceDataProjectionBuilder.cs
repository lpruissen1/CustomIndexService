using Core;
using Core.Extensions;
using MongoDB.Driver;
using StockScreener.Database.Model.Price;
using System;
using System.Linq;

namespace Database.Repositories
{
	public class PriceDataProjectionBuilder : IPriceDataProjectionBuilder
    {
		public PriceDataProjectionBuilder() { }

		public ProjectionDefinition<TPriceData> BuildProjection<TPriceData>(TimePeriod timePeriod) where TPriceData : PriceData
		{
			var now = ((double)DateTimeOffset.Now.ToUnixTimeSeconds());
			var timeRange = (now - TimePeriodConverter.GetUnixFromTimePeriod(timePeriod));
			var projection = Builders<TPriceData>.Projection;

			projection.Include(x => x.Candle.Where(candle => candle.timestamp > timeRange));
			projection.Include(x => x.Ticker);
			projection.Include(x => x.Version);

			return projection.Combine();
        }
    }
}
