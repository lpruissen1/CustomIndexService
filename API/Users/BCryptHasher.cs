using System;
using BC = BCrypt.Net.BCrypt;

namespace Users
{
	public class BCryptHasher : IHasher
	{

		public bool Check(string phrase, string hash)
		{
			if (phrase is null || phrase == "")
				throw new Exception("No match");

			if (BC.Verify(phrase, hash))
				return true;

			return false;
		}

		public string Hash(string phrase)
		{
			if (phrase is null || phrase == "")
				throw new Exception("Empty Hash Request");

			var hash = BC.HashPassword(phrase, 12);

			return hash;
		}
	}
}
