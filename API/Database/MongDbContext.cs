using Database.Config;
using MongoDB.Driver;

namespace Database
{
    public class MongoDbContext : IMongoCustomIndexDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDbContext(IUserInfoDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
