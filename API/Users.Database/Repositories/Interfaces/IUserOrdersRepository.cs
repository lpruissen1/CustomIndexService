using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserOrdersRepository : IBaseRepository<UserOrders>
	{
		UserOrders GetByUserId(Guid userId);
	}
}
