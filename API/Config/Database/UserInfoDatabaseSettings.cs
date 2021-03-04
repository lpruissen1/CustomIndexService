namespace Config.Models
{
    public class UserInfoDatabaseSettings : IUserInfoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CustomIndexCollectionName { get; set; }
    }
}