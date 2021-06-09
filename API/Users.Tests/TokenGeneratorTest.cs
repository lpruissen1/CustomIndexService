using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Users.Core;

namespace Users.Tests
{
	[TestFixture]
	public class TokenGeneratorTest : UsersTestBase
    {
		private IJwtConfiguration tokenGeneratorConfig;
		private ITokenGenerator sut;

		[SetUp]
		public void SetUp()
		{
			tokenGeneratorConfig = new JwtConfiguration() {Key = config["JwtConfiguration:Key"], Expiration = config["JwtConfiguration:Expiration"], Issuer = config["JwtConfiguration:Issuer"] };
			sut = new TokenGenerator(tokenGeneratorConfig);
		}

		[Test]
        public void GeneratorJsonWebToken_ReturnsTokenAssociatedWithUser()
        {
			var userId = Guid.NewGuid().ToString();

			var token = sut.GeneratorJsonWebToken(userId);

			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
			var userIdClaim = jsonToken.Claims.First(x => x.Type == "nameid").Value;

			Assert.AreEqual(userId, userIdClaim);
        }
    }

	public class UsersTestBase
	{
		protected IConfigurationRoot config;

		[SetUp]
		protected void SetUp()
		{
			config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\Users.Tests").AddJsonFile("appsettings.json").Build();
		}

	}
}
