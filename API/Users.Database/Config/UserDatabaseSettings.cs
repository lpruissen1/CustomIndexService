namespace Users.Database.Config
{
	public class UserDatabaseSettings : IUserDatabaseSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}
