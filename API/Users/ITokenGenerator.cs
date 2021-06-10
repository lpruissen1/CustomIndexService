namespace Users
{
	public interface ITokenGenerator
	{
		string GeneratorJsonWebToken(string userId);
	}
}
