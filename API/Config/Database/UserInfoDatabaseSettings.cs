namespace Config.Models
{
    public class UserInfoDatabaseSettings : IUserInfoDatabaseSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CustomIndexCollectionName { get; set; }
        public string UserIndicesCollectionName { get; set; }
    }
}