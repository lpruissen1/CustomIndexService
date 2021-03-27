namespace Config.Models
{
    public interface IUserInfoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CustomIndexCollectionName { get; set; }
    }
}