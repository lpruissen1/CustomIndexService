namespace Users.Core.Request
{
	public class CreateUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
	}

	public class LoginRequest
	{
		public string Username { get; set; }
		public string PasswordHash { get; set; }
	}
}
