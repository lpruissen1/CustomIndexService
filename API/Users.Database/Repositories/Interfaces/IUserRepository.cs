using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		User Get(Guid userId);
		User GetByUsername(string userName);
		User GetByUserId(string userId);
	}
}
