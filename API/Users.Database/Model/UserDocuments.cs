using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserDocuments : DbEntity
	{
		public string UserId { get; set; }
		public List<Document> Documents { get; set; } = new List<Document>();
	}

	public class UserOrders : DbEntity
	{
		public string UserId { get; set; }
		public List<Order> Orders { get; set; } = new List<Order>();
	}

	public class Order
	{
		[BsonRepresentation(BsonType.String)]
		public Guid OrderId { get; set; }
		[BsonRepresentation(BsonType.String)]
		public Guid? TransactionId { get; set; }
		public string Ticker { get; set; }
		[BsonRepresentation(BsonType.String)]
		public OrderStatusValue Status { get; set; }
		[BsonRepresentation(BsonType.String)]
		public OrderType Type { get; set; }
		[BsonRepresentation(BsonType.String)]
		public OrderDirectionValue Side { get; set; }
		[BsonRepresentation(BsonType.String)]
		public OrderExecutionTimeframeValue Time_in_force { get; set; }
		public decimal? OrderedQuantity { get; set; }
		public decimal? OrderedAmount { get; set; }
		public decimal? FilledQuantity { get; set; }
		public decimal? FilledAmount { get; set; }
	}
}
