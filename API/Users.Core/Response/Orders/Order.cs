using Core;
using System;

namespace Users.Core.Response.Orders
{
	public class Order
	{
		public Guid OrderId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? FilledAt { get; set; }
		public string Ticker { get; set; }
		public OrderStatusValue Status { get; set; }
		public OrderType Type { get; set; }
		public OrderDirectionValue Side { get; set; }
		public decimal? OrderedQuantity { get; set; }
		public decimal? OrderedAmount { get; set; }
		public decimal? FilledQuantity { get; set; }
		public decimal? FilledAmount { get; set; }
	}
}
