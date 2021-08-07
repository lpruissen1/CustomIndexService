using Microsoft.AspNetCore.Mvc;
using Users.Core;

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

        [HttpGet("Info")]
        public IActionResult Info(string userId)
        {
			return userService.GetInfo(userId);
        }
    }
}
