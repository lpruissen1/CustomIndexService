namespace Users
{
	public interface IHasher
	{
		string Hash(string password);
		bool Check(string password, string hash);
	}
}
