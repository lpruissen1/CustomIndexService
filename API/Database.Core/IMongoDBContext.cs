using MongoDB.Driver;

namespace Database.Core
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void ClearAll();
        void DropCollection(string name);
    }
}