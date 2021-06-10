using System;

namespace Users.Core
{
	public interface IJwtConfiguration
	{
		string Key { get; set; }
		string Issuer { get; set; }
		string Expiration { get; set; }
	}

	public class JwtConfiguration : IJwtConfiguration
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Expiration { get; set; }
	}
}
