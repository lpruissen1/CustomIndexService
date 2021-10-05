using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserAccountsRepository : IBaseRepository<UserAccounts>
	{
		UserAccounts GetByUserId(Guid userId);
		UserAccounts GetByAccountId(Guid accountId);
	}
}
