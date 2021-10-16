using System.Collections.Generic;
using Users.Core.Response.Orders;

namespace Users.Orders
{
	public static class OrderMapper
	{
		public static OrderResponse MapOrders(IEnumerable<Database.Model.Order> orders)
		{
			var response = new OrderResponse();

			foreach (var order in orders)
			{
				response.Orders.Add(new Core.Response.Orders.Order
				{
					OrderId = order.OrderId,
					CreatedAt = order.CreatedAt,
					FilledAt = order.FilledAt,
					Ticker = order.Ticker,
					Status = order.Status,
					Type = order.Type,
					Side = order.Side,
					OrderedQuantity = order.OrderedQuantity,
					OrderedAmount = order.OrderedAmount,
					FilledQuantity = order.FilledQuantity,
					FilledAmount = order.FilledAmount
				});
			}

			return response;
		}
	}
}
