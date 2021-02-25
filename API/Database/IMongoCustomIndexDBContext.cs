using MongoDB.Driver;

namespace Database
{
    public interface IMongoCustomIndexDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}