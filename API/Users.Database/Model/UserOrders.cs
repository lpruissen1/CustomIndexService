using Database.Core;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserOrders : DbEntity
	{
		public string UserId { get; set; }
		public List<Order> Orders { get; set; } = new List<Order>();
	}
}
