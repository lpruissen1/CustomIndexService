using System;
using System.Collections.Generic;

namespace Users.Core.Request
{
	public class BulkPurchaseRequest
	{
		public Guid PortfolioId { get; set; }
		public List<OrderEntry> Orders { get; set; } = new List<OrderEntry>();
	}
}
