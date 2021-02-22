namespace Database.Config
{
    public interface IUserInfoDatabaseSettings
    {
        string CustomIndexCollectionName { get; set; }
        string UserIndicesCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}