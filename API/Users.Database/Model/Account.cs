using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Users.Database.Model
{
	public class Account
	{
		[BsonRepresentation(BsonType.String)]
		public Guid AccountId { get; set; }
		public string AccountDisplayId{ get; set; }
		public string Institution { get; set; }
		public DateTime DateRegistered { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Active { get; set; }
	}
}
