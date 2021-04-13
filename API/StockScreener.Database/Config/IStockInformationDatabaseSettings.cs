namespace StockScreener.Database.Config
{
    public interface IStockInformationDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}