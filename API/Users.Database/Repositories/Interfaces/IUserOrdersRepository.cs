using Database.Core;
using System;
using System.Collections.Generic;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserOrdersRepository : IBaseRepository<UserOrders>
	{
		UserOrders GetByUserId(Guid userId);
		void AddOrders(Guid userId, List<Order> orders);
		void AddOrder(Guid userId, Order order);
		void FillOrder(Guid userId, Guid orderId, Order order);
		void UpdateOrderStatus(Guid userId, Order order);
	}
}
