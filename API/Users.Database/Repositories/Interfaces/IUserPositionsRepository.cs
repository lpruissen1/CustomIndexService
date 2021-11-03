using Database.Core;
using System;
using Users.Database.Model;

namespace Users.Database.Repositories.Interfaces
{
	public interface IUserPositionsRepository : IBaseRepository<UserPositions>
	{
		UserPositions GetByUserId(Guid userId);
		void CreatePosition(Guid userId, Position position);
		void UpdatePosition(Guid userId, Position position);
		void UpdatePosition(Guid userId, UserPositions position);
	}
}
