namespace Users.Core.Request
{
	public class LoginRequest
	{
		public string Username { get; set; }
		public string PasswordHash { get; set; }
	}
}
