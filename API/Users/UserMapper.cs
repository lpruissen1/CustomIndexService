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
				UserId = Guid.NewGuid().ToString(),
				AccountType = "Basic",
				UserName = request.UserName, 
				EmailAddress = request.Email, 
				FirstName = request.FirstName, 
				LastName = request.LastName 
			};
		}

		public static User MapUpgradeUserRequest(UpgradeUserRequest request, User user)
		{
			return new User
			{
				UserId = user.UserId,
				AccountType = "Premium",
				UserName = user.UserName,
				EmailAddress = user.EmailAddress,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = request.PhoneNumber,
				StreetAddress = request.StreetAddress,
				City = request.City,
				State = request.State,
				PostalCode = request.PostalCode,
				DateOfBirth = request.DateOfBirth,
				CountryOfTaxResidency = request.CountryOfTaxResidency
			};
	}

		internal static User MapCreateUserRequest(UpgradeUserRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
