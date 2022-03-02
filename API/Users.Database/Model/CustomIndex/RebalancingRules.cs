using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Users.Core;

namespace Users.Database.Model.CustomIndex
{
	public class RebalancingRules
	{
		[BsonRepresentation(BsonType.String)]
		public RebalancingFrequency Frequency { get; init; }
		public bool Automatic { get; init; }
	}
}
