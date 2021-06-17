using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
	public class RangedRule : DbEntity
    {
        [BsonRepresentation(BsonType.String)]
        public RuleType RuleType;
        public List<Range> Ranges;
    }
}