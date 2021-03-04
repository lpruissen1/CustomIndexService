using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Database.Model
{
    public abstract class DbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public abstract string GetPrimaryKey();
    }
}
