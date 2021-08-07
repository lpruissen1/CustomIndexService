using Microsoft.AspNetCore.Mvc;
using Users.Core.Request;

namespace Users.Core
{
	public interface IUserService
	{
		public IActionResult CreateBasicUser(CreateUserRequest request);
		public IActionResult Login(LoginRequest request);
		public IActionResult GetInfo(string userId);
		public IActionResult UpgradeUser(UpgradeUserRequest request);
	}
}
