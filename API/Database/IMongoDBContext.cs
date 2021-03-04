using MongoDB.Driver;

namespace Database
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void ClearAll();
    }
}