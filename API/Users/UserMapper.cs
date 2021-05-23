using System;
using Users.Core.Request;
using Users.Database.Model;

namespace Users
{
	public static class UserMapper
	{
		public static User MapCreateUserRequest(CreateUserRequest request)
		{
			return new User {
				UserId = Guid.NewGuid(),
				UserName = request.UserName, 
				EmailAddress = request.Email, 
				FirstName = request.FirstName, 
				LastName = request.LastName 
			};
		}
	}
}
