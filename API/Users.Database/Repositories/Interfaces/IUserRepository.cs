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
		bool UpgradeUser(User request);
	}

	public interface IUserDisclosuresRepository : IBaseRepository<UserDisclosures>
	{
		UserDisclosures GetByUserId(string userId);
	}
	public interface IUserAccountsRepository : IBaseRepository<UserAccounts>
	{
		UserAccounts GetByUserId(string userId);
	}
	public interface IUserDocumentsRepository : IBaseRepository<UserDocuments>
	{
		UserDocuments GetByUserId(string userId);
	}
}
