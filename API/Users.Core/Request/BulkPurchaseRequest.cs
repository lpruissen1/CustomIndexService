using System.Collections.Generic;

namespace Users.Core.Request
{
	public class BulkPurchaseRequest
	{
		public List<OrderEntry> Orders { get; set; } = new List<OrderEntry>();
	}
}
