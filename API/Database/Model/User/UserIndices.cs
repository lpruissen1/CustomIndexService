using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Database.Model.User
{
    public class UserIndices
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid userId { get; set; }

        public List<string> indexId{ get; set; }
    }
}
