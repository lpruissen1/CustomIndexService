using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserDisclosuresRepository : IBaseRepository<UserDisclosures>
	{
		UserDisclosures GetByUserId(Guid userId);
	}
}
