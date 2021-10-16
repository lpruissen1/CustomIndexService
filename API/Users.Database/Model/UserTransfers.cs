using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserTransfers : DbEntity
	{
		public UserTransfers(Guid userId)
		{
			UserId = userId;
		}

		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; init; }
		public List<Transfer> Transfers { get; set; } = new List<Transfer>();
	}
}
