using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Core
{
	public class DbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
    }
}
