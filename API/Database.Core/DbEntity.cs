using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Core
{
    public class DbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

    public class StockDbEntity : DbEntity
    {
        public string Ticker { get; set; }
    }
}
