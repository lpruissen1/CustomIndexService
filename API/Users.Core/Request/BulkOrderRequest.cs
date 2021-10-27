using Core;
using System;
using System.Collections.Generic;

namespace Users.Core.Request
{
	public class BulkOrderRequest
	{
		public Guid PortfolioId { get; set; }
		public List<OrderEntry> Orders { get; set; } = new List<OrderEntry>();
		public OrderDirectionValue Side { get; set; }
	}
}
