using Microsoft.AspNetCore.Mvc;
using Users.Core;
using Users.Core.Request;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
		private readonly IUserService userService;

		public AuthController(IUserService userService)
        {
			this.userService = userService;
		}

        [HttpPost("create")]

        public IActionResult Create(CreateUserRequest request)
        {
			return userService.CreateUser(request);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
			return userService.Login(request);
        }
    }
}
