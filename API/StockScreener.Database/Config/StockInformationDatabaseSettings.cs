namespace StockScreener.Database.Config
{
    public class StockInformationDatabaseSettings : IStockDataDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}