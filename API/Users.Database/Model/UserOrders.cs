using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserOrders : DbEntity
	{
		public UserOrders(Guid userID)
		{
			UserId = userID;
		}

		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; set; }

		public List<Order> Orders { get; set; } = new List<Order>();
	}
}
