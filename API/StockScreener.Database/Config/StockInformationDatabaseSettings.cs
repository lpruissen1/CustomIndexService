namespace StockScreener.Database.Config
{
    public class StockInformationDatabaseSettings : IStockInformationDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}