namespace StockScreener.Database.Config
{
    public class PriceDatabaseSettings : IPriceDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}