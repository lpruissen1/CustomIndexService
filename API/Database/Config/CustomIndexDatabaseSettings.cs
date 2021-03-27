namespace Database.Config
{
    public class CustomIndexDatabaseSettings : ICustomIndexDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CustomIndexCollectionName { get; set; }
    }
}