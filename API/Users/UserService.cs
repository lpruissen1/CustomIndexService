using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger logger;

		private const string invalidCredentialsMessage = "Invalid Credentials";

		public UserService(IUserRepository userRepository, IPasswordListRepository passwordListRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, ILogger logger)
		{
			this.userRepository = userRepository;
			this.passwordListRepository = passwordListRepository;
			this.passwordHasher = passwordHasher;
			this.tokenGenerator = tokenGenerator;
			this.logger = logger;
		}

		public IActionResult CreateUser(CreateUserRequest request)
		{
			var user = userRepository.Create(UserMapper.MapCreateUserRequest(request));
			var hashedPassword = passwordHasher.Hash(request.Password);

			passwordListRepository.Create(new PasswordList() { UserId = user.UserId, CurrentPassword = hashedPassword });
			
			logger.LogInformation(new EventId(1), $"User created with username: {user.UserName}");

			return new OkObjectResult(new LoginResponse() { Token = GetToken(user.UserId), UserID = user.UserId });
		}

		public IActionResult Login(LoginRequest request)
		{
			var userId = userRepository.GetByUsername(request.Username)?.UserId;

			if (userId is null)
			{
				logger.LogInformation(new EventId(1), invalidCredentialsMessage);
				return new BadRequestObjectResult(invalidCredentialsMessage);
			}

			var currentPasswordHash = passwordListRepository.Get(userId).CurrentPassword;

			if (passwordHasher.Check(request.Password, currentPasswordHash))
			{
				var token = GetToken(userId);
				return new OkObjectResult(new LoginResponse() { Token = token, UserID = userId });
			}

			return new BadRequestObjectResult(invalidCredentialsMessage);
		}

		private string GetToken(string userId)
		{
			return tokenGenerator.GeneratorJsonWebToken(userId);
		}
	}
}
