using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class TimedRange : Range
    {
        [BsonRepresentation(BsonType.String)]
        public TimePeriod TimePeriod;
    }
}