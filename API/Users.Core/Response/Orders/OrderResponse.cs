using System.Collections.Generic;

namespace Users.Core.Response.Orders
{
	public class OrderResponse
	{
		public List<Order> Orders { get; } = new List<Order>();
	}
}
