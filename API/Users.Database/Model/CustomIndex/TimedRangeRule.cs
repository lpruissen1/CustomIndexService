using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Users.Database.Model.CustomIndex
{
	public class TimedRangeRule : DbEntity 
    {
        [BsonRepresentation(BsonType.String)]
        public RuleType RuleType;
		//Dont return list. 
        public List<TimedRange> TimedRanges;
    }
}
