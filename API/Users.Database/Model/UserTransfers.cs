using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserTransfers : DbEntity
	{
		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; set; }
		public List<Transfer> Transfers { get; set; } = new List<Transfer>();
	}
}
