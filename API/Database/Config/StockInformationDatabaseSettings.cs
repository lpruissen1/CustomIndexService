namespace Database.Config
{
    public class StockInformationDatabaseSettings : IStockDataDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CompanyInfoCollectionName { get; set; }
    }
}