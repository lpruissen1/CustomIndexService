using Microsoft.AspNetCore.Mvc;
using Users.Core.Request;

namespace Users.Core
{
	public interface IUserService
	{
		public IActionResult CreateUser(CreateUserRequest request);
		public IActionResult Login(LoginRequest request);
	}
}
