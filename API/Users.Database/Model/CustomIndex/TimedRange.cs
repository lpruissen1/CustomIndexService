using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Users.Database.Model.CustomIndex;

namespace Users.Database.Model.CustomIndex
{
	public class TimedRange : Range
    {
        [BsonRepresentation(BsonType.String)]
        public TimePeriod TimePeriod;
    }
}
