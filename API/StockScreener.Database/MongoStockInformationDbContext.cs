﻿using Database.Core;
using MongoDB.Driver;
using StockScreener.Database.Config;

namespace StockScreener.Database
{
    public class MongoStockInformationDbContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        private IStockInformationDatabaseSettings settings { get; set; }

        public MongoStockInformationDbContext(IStockInformationDatabaseSettings settings)
        {
            this.settings = settings;
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public void ClearAll()
        {
            _mongoClient.DropDatabase(settings.DatabaseName);
        }

        public void DropCollection(string name)
        {
            _db.DropCollection(name);
        }
    }
}
