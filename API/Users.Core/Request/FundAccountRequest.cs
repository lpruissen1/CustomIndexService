using Core;
using System;
using System.Collections.Generic;

namespace Users.Core.Request
{
	public class FundAccountRequest
	{
		public TransferTypeValue TransferType { get; set; }
		public Guid RelationshipId { get; set; }
		public string Amount { get; set; }
		public TransferDirectionValue Direction { get; set; }
	}

	public class BulkPurchaseRequest
	{
		public List<OrderEntry> Orders { get; set; } = new List<OrderEntry>();
	}

	public class OrderEntry
	{
		public string Ticker { get; set; }
		public decimal Amount { get; set; }
	}
}
