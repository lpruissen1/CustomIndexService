using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserDocumentsRepository : IBaseRepository<UserDocuments>
	{
		UserDocuments GetByUserId(Guid userId);
	}
}
