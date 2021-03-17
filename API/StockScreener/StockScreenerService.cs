using Database.Model.User.CustomIndices;
using StockScreener.Mapper;
using StockScreener.SecurityGrabber;

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
            var metricList = mapper.MapToMetricList(index);

            var queryParams = new SecuritiesSearchParams { Indices = metricList.Indices, Datapoints = metricList.GetRelevantDatapoints() };
            var securities = securitiesGrabber.GetSecurities(queryParams);

            metricList.Apply(ref securities);
            
            return securities;
            // option 1. create huge filter within and filter the collection independently and returing a complete list of stocks (currently implemented)
            // option 2. Get list of securities and filter them metric by metric. Needs datapoint list prior

            // return null;
        }
    }
}
