using System;
using Users.Core.Response.Orders;

namespace Users.Core
{
	public interface IOrderService
	{
		OrderResponse GetOrders(Guid userId);
		bool CancelOrder(Guid userId, Guid transferId);
	}
}
