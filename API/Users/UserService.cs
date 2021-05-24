using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Core;
using Users.Core.Request;
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
		private IJwtConfiguration jwtConfig;

		public UserService(IUserRepository userRepository, IPasswordListRepository passwordListRepository, IJwtConfiguration jwtConfig)
		{
			this.userRepository = userRepository;
			this.passwordListRepository = passwordListRepository;
			this.jwtConfig = jwtConfig;
		}

		public IActionResult CreateUser(CreateUserRequest request)
		{
			var user = userRepository.Create(UserMapper.MapCreateUserRequest(request));
			passwordListRepository.Create(new PasswordList() { UserId = user.UserId, CurrentPassword = request.PasswordHash });

			var token = GenerateJSONWebToken(user.UserId.ToString());

			return new OkObjectResult(GenerateJSONWebToken(user.UserId.ToString()));
		}

		public IActionResult Login(LoginRequest request)
		{
			var userId = userRepository.GetByUsername(request.Username).UserId;
			var currentPassword = passwordListRepository.Get(userId).CurrentPassword;
			if (currentPassword == request.PasswordHash)
			{
				var token = GenerateJSONWebToken(userId.ToString());
				return new OkObjectResult(token);
			}

			return new BadRequestObjectResult("Get fucked nerd");
		}

		private string GenerateJSONWebToken(string userId)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
	
			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.NameId, userId),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken (jwtConfig.Issuer,
			  jwtConfig.Issuer,
			  claims,
			  expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtConfig.Expiration)),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
