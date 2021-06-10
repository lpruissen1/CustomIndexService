using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Core;

namespace Users
{
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
}
