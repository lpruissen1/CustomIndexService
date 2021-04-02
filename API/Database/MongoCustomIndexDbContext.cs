using Database.Config;
using Database.Core;
using MongoDB.Driver;

namespace Database
{
    public class MongoCustomIndexDbContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        private ICustomIndexDatabaseSettings settings { get; set; }
        public MongoCustomIndexDbContext(ICustomIndexDatabaseSettings settings)
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
            throw new System.NotImplementedException();
        }
    }
}
