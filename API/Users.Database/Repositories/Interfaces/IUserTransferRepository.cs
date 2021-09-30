using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserTransfersRepository : IBaseRepository<UserTransfers>
	{
		UserTransfers GetByUserId(Guid userId);
		void AddTransfer(Guid userId, Transfer transfer);
		void UpdateTransfer(Guid userId, Transfer transfer);
	}
}
