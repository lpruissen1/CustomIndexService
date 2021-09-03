using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Users.Database.Model
{
	public class AchRelationship
	{
		[BsonRepresentation(BsonType.String)]
		public Guid Id { get; set; }
		public string Nickname{ get; set; }
		public AchStatusValue Status { get; set; }
	}
}
