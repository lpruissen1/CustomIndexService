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

		public void Create(HourPriceData obj)
		{
			hourIntervalCollection.InsertOne(obj);
		}

		public void Create(DayPriceData obj)
		{
			dayIntervalCollection.InsertOne(obj);
		}

		public void Update(DayPriceData entry)
		{
			UpdatePriceData(entry);
		}

		public void Update(HourPriceData entry)
		{
			UpdatePriceData(entry);
		}

		public double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData
		{
			var filter = Builders<TPriceEntry>.Filter.Eq(e => e.Ticker, ticker);
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(filter).FirstOrDefault();
			return prices?.Candle.Max(x => x.timestamp) ?? 0;
		}
		public List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData
		{
			var filter = Builders<TPriceEntry>.Filter.Eq(e => e.Ticker, ticker);
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(filter).FirstOrDefault();
			return prices.Candle;
		}

		private void UpdatePriceData<TPriceType>(TPriceType entry) where TPriceType : PriceData
		{
			var filter = Builders<TPriceType>.Filter.Eq(e => e.Ticker, entry.Ticker);

			var updateDefinition = new List<UpdateDefinition<TPriceType>>();

			updateDefinition.Add(AddCandleUpdate<TPriceType>(entry.Candle));

			var combinedUpdate = Builders<TPriceType>.Update.Combine(updateDefinition);

			mongoContext.GetCollection<TPriceType>(typeof(TPriceType).Name).FindOneAndUpdate<TPriceType>(filter, combinedUpdate);
		}
    
		private UpdateDefinition<TPriceType> AddCandleUpdate<TPriceType>(List<Candle> candles)
		{
			return Builders<TPriceType>.Update.PushEach<Candle>("Candle", candles);
		}

    public List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData
		{
			var filter = Builders<TPriceEntry>.Filter.Eq(e => e.Ticker, ticker);
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(filter).FirstOrDefault();
			return prices.Candle;
		}
  }
}
