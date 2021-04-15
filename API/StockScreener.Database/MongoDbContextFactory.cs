using Database.Core;
using Microsoft.Extensions.Configuration;
using StockScreener.Database.Config;

namespace StockScreener.Database
{
    public class MongoDbContextFactory
    {
        private IConfigurationRoot config;

        public MongoDbContextFactory()
        {
            config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\AggregationService").AddJsonFile("appsettings.json").Build();
        }

        public IMongoDBContext GetPriceContext()
        {
            var priceDBSettings = new StockInformationDatabaseSettings() { ConnectionString = config["PriceDatabaseSettings:ConnectionString"], DatabaseName = config["PriceDatabaseSettings:DatabaseName"] };
            return new MongoStockInformationDbContext(priceDBSettings);
        }

        public IMongoDBContext GetStockContext()
        {
            var stockDBSettings = new StockInformationDatabaseSettings() { ConnectionString = config["StockDatabaseSettings:ConnectionString"], DatabaseName = config["StockDatabaseSettings:DatabaseName"] };
            return new MongoStockInformationDbContext(stockDBSettings);
        }
    }
}
