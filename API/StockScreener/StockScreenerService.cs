using Database.Model.User.CustomIndices;
using StockScreener.Mapper;

namespace StockScreener
{
    public class StockScreenerService
    {
        private readonly ISecuritiesGrabber securitiesGrabber;

        public StockScreenerService(ISecuritiesGrabber securitiesGrabber)
        {
            this.securitiesGrabber = securitiesGrabber;
        }

        public SecuritiesList Screen(CustomIndex index)
        {
            var mapper = new CustomIndexMapper();
            //var securitiesGrabber = new SecuritiesGrabber(new CompanyInfoRepository( new MongoStockInformationDbContext(new StockDataDatabaseSettings { ConnectionString = "mongodb://localhost:27017", DatabaseName = "StockData" })));
            var metricList = mapper.MapToMetricList(index);
            return  securitiesGrabber.GetSecurities(metricList); // option 1. create huge filter within and filter the collection independently and returing a complete list of stocks
            //securitiesGrabber.GetSecurities(metricList);  option 2. Get list of securities and filter them metric by metric. Needs datapoint list prior

            // return null;
        }
    }
}
