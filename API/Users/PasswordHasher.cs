using System;
using BC = BCrypt.Net.BCrypt;

namespace Users
{
	public class PasswordHasher : IPasswordHasher
	{

		public bool Check(string password, string hash)
		{
			if (password is null || password == "")
				throw new Exception("Invalid password for verification");

			if (BC.Verify(password, hash))
				return true;

			return false;
		}

		public string Hash(string password)
		{
			if (password is null || password == "")
				throw new Exception("Invalid password for hashing");

			var hash = BC.HashPassword(password, 12);

			return hash;
		}
	}
}
