using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using UserCustomIndices.Core;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class TimedRangeRule : DbEntity 
    {
        [BsonRepresentation(BsonType.String)]
        public RuleType RuleType;
		//Dont return list. 
        public List<TimedRange> TimedRanges;
    }
}
