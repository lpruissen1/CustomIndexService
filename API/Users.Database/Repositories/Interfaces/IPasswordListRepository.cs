using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IPasswordListRepository : IBaseRepository<PasswordList>
	{
		PasswordList Get(Guid userId);
		PasswordList Update(PasswordList passwordList);
	}
}
