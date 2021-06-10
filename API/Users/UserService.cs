using Microsoft.AspNetCore.Mvc;
using Users.Core;
using Users.Core.Request;
using Users.Core.Response;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users
{
	// map request model to db model
	// nay logic for updating and creating in here
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		private readonly IPasswordListRepository passwordListRepository;
		private readonly IPasswordHasher passwordHasher;
		private readonly ITokenGenerator tokenGenerator;

		public UserService(IUserRepository userRepository, IPasswordListRepository passwordListRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
		{
			this.userRepository = userRepository;
			this.passwordListRepository = passwordListRepository;
			this.passwordHasher = passwordHasher;
			this.tokenGenerator = tokenGenerator;
		}

		public IActionResult CreateUser(CreateUserRequest request)
		{
			var user = userRepository.Create(UserMapper.MapCreateUserRequest(request));
			var hashedPassword = passwordHasher.Hash(request.Password);

			passwordListRepository.Create(new PasswordList() { UserId = user.UserId, CurrentPassword = hashedPassword });

			return new OkObjectResult(new LoginResponse() { Token = GetToken(user.UserId), UserID = user.UserId });
		}

		public IActionResult Login(LoginRequest request)
		{
			var userId = userRepository.GetByUsername(request.Username)?.UserId;

			if(userId is null)
				return new BadRequestObjectResult("Invalid Credentials");

			var currentPasswordHash = passwordListRepository.Get(userId).CurrentPassword;

			if (passwordHasher.Check(request.Password, currentPasswordHash))
			{
				var token = GetToken(userId);
				return new OkObjectResult(new LoginResponse() { Token = token, UserID = userId });
			}

			return new BadRequestObjectResult("Invalid Credentials");
		}

		private string GetToken(string userId)
		{
			return tokenGenerator.GeneratorJsonWebToken(userId);
		}
	}
}
