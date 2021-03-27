namespace Database.Config
{
    public interface ICustomIndexDatabaseSettings
    {   
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CustomIndexCollectionName { get; set; }
    }
}