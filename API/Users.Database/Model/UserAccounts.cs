using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserAccounts : DbEntity
	{
		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; set; }
		public List<Account> Accounts { get; set; } = new List<Account>();
	}
}
