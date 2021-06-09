using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
			// and handling here for unfound usernames
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

	public class TokenGenerator : ITokenGenerator
	{
		private readonly IJwtConfiguration jwtConfig;

		public TokenGenerator(IJwtConfiguration jwtConfig)
		{
			this.jwtConfig = jwtConfig;
		}

		public string GeneratorJsonWebToken(string userId)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.NameId, userId),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken(jwtConfig.Issuer,
			  jwtConfig.Issuer,
			  claims,
			  
			  expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtConfig.Expiration)),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}

	public interface ITokenGenerator
	{
		string GeneratorJsonWebToken(string userId);
	}
}
