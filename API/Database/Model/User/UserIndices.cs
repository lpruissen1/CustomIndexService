using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Database.Model.User
{
    public class UserIndices : DbEntity
    {
        public Guid UserId { get; set; }

        public List<string> IndexId{ get; set; }

        public override string GetPrimaryKey()
        {
            return Id;            
        }
    }
}
