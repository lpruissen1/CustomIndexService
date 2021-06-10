using Database.Core;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IPasswordListRepository : IBaseRepository<PasswordList>
	{
		PasswordList Update(PasswordList passwordList);
	}
}
