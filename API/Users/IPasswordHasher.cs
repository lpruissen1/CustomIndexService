﻿namespace Users
{
	public interface IPasswordHasher
	{
		string Hash(string password);
		bool Check(string password, string hash);
	}
}
