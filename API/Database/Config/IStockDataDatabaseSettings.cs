namespace Database.Config
{
    public interface IStockDataDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CompanyInfoCollectionName { get; set; }
    }
}