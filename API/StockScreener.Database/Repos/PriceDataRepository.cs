using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database.Model.Price;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{

    public class PriceDataRepository : BaseRepository<PriceData>, IPriceDataRepository
	{
		protected IMongoCollection<HourPriceData> hourIntervalCollection;
		protected IMongoCollection<DayPriceData> dayIntervalCollection;

		public PriceDataRepository(IMongoDBContext context) : base(context)
		{
			dayIntervalCollection = mongoContext.GetCollection<DayPriceData>(typeof(DayPriceData).Name);
			hourIntervalCollection = mongoContext.GetCollection<HourPriceData>(typeof(HourPriceData).Name);
		}


		public double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData
		{
			var filter = Builders<TPriceEntry>.Filter.Eq(e => e.Ticker, ticker);
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(filter).FirstOrDefault();
			return prices?.Candle.Max(x => x.timestamp) ?? 0;
		}

        public double GetPriceData<TPriceEntry>(string ticker, TimeSpan timeSpan) where TPriceEntry : PriceData
        {
            throw new NotImplementedException();
        }

        public List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData
		{
			var filter = Builders<TPriceEntry>.Filter.Eq(e => e.Ticker, ticker);
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(filter).FirstOrDefault();
			return prices.Candle;
		}
    }
}
