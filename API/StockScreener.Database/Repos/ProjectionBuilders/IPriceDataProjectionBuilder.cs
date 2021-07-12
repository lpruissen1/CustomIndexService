using Core;
using MongoDB.Driver;
using StockScreener.Database.Model.Price;

namespace Database.Repositories
{
	public interface IPriceDataProjectionBuilder
	{
		public ProjectionDefinition<TPriceData> BuildProjection<TPriceData>(TimePeriod timePeriod) where TPriceData : PriceData;
	}
}
