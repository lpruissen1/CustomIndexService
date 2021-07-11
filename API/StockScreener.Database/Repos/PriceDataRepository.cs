using Core;
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
		public PriceDataRepository(IMongoDBContext context) : base(context) { }

		public PriceDataRepository(IMongoDbContextFactory contextFactory) : base(contextFactory.GetPriceContext()) { }

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
			var prices = mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(x => x.Ticker == ticker).FirstOrDefault();

			return prices?.Candle ?? new List<Candle>();
		}

        public IEnumerable<TPriceEntry> GetClosePriceOverTimePeriod<TPriceEntry>(IEnumerable<string> tickers, TimePeriod timeSpan) where TPriceEntry : PriceData
		{
			var tickerFilter = Builders<TPriceEntry>.Filter.In(e => e.Ticker, tickers);
			var timeStampFilter = Builders<TPriceEntry>.Filter.In(e => e.Ticker, tickers);
			var combinedFilter = Builders<TPriceEntry>.Filter.And(tickerFilter, timeStampFilter);

			return mongoContext.GetCollection<TPriceEntry>(typeof(TPriceEntry).Name).Find(combinedFilter).ToEnumerable();
        }

		private void UpdatePriceData<TPriceType>(TPriceType entry) where TPriceType : PriceData
		{
			var filter = Builders<TPriceType>.Filter.Eq(e => e.Ticker, entry.Ticker);

			var updateDefinition = new List<UpdateDefinition<TPriceType>>();

			updateDefinition.Add(AddCandleUpdate<TPriceType>(entry.Candle));

			var combinedUpdate = Builders<TPriceType>.Update.Combine(updateDefinition);

			mongoContext.GetCollection<TPriceType>(typeof(TPriceType).Name).FindOneAndUpdate(filter, combinedUpdate, new FindOneAndUpdateOptions<TPriceType, TPriceType>() { IsUpsert = true});
		}
    
		private UpdateDefinition<TPriceType> AddCandleUpdate<TPriceType>(List<Candle> candles)
		{
			return Builders<TPriceType>.Update.PushEach<Candle>("Candle", candles);
		}
    }
}
