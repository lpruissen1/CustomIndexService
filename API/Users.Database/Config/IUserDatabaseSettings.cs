namespace Users.Database.Config
{
	public interface IUserDatabaseSettings
	{
		string DatabaseName { get; set; }
		string ConnectionString { get; set; }
	}
}
