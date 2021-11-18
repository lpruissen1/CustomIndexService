using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Users.Database.Model.CustomIndex;

namespace Users.Database.Model.CustomIndex
{
	public class RangedRule : DbEntity
    {
        [BsonRepresentation(BsonType.String)]
        public RuleType RuleType;
        public List<Range> Ranges;
    }
}
