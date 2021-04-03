namespace Config.Models
{
	public interface IStockInformationDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CompanyInfoCollectionName { get; set; }
    }
}