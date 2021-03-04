namespace Config.Models
{
    public class StockInformationDatabaseSettings : IStockInformationDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CompanyInfoCollectionName { get; set; }
    }
}