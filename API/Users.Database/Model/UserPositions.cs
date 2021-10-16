using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserPositions : DbEntity
	{
		public UserPositions(Guid userId)
		{
			UserId = userId;
		}

		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; init; }

		public List<Position> Positions { get; set; } = new List<Position>();
	}
}
