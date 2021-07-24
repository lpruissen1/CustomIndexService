using Microsoft.AspNetCore.Mvc;
using Users.Core;
using Users.Core.Request;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
		private readonly IUserService userService;

		public UserController(IUserService userService)
        {
			this.userService = userService;
		}

        [HttpPost("getInfo")]
        public IActionResult GetInfo(string userId)
        {
			return userService.GetInfo(userId);
        }
    }
}
