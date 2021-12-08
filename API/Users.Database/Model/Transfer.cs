using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Users.Database.Model
{
	public class Transfer
	{
		[BsonRepresentation(BsonType.String)]
		public Guid AccountId { get; set; }

		[BsonRepresentation(BsonType.String)]
		public Guid TransferId{ get; set; }

		public decimal Amount { get; set; }

		[BsonRepresentation(BsonType.DateTime)]
		public DateTime CreatedAt { get; set; }

		[BsonRepresentation(BsonType.DateTime)]
		public DateTime CompletedAt { get; set; }

		[BsonRepresentation(BsonType.DateTime)]
		public DateTime CancelledAt { get; set; }

		[BsonRepresentation(BsonType.String)]
		public TransferStatusValue Status { get; set; }

		[BsonRepresentation(BsonType.String)]
		public TransferDirectionValue Direction { get; set; }

		[BsonRepresentation(BsonType.String)]
		public TransferTypeValue TransferType { get; set; }
	}
}
