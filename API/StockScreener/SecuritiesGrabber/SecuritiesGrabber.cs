using StockScreener.Core;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class SecuritiesGrabber : ISecuritiesGrabber
    {
        private readonly ICompanyInfoRepository companyInfoRespository;
        private readonly IStockFinancialsRepository stockFinancialsRespository;
        private readonly IStockIndexRepository stockIndicesRespository;

        private SecuritiesList list;

        public SecuritiesGrabber(IStockFinancialsRepository stockFinancialsRespository, ICompanyInfoRepository companyInfoRespository, IStockIndexRepository stockIndicesRespository)
        {
            this.companyInfoRespository = companyInfoRespository;
            this.stockFinancialsRespository = stockFinancialsRespository;
            this.stockIndicesRespository = stockIndicesRespository;
        }

        public SecuritiesList GetSecurities(SecuritiesSearchParams searchParams)
        {
            list = PullBaseSecurityList(searchParams.Indices);

            AddCompanyInfo(searchParams.Datapoints);
            AddStockFinancials(searchParams.Datapoints);

            return list;
        }

        private void AddCompanyInfo(IEnumerable<Datapoint> datapoints)
        {
            if ( !datapoints.Any() )
                return;

            foreach ( var security in list )
            {
                var companyInfo = companyInfoRespository.Get(security.Ticker, datapoints);
                if ( companyInfo is not null )
                {
                    security.Industry = companyInfo.Industry;
                    security.Sector = companyInfo.Sector;
                }
            }
        }

        private void AddStockFinancials(IEnumerable<Datapoint> datapoints)
        {
            if ( !datapoints.Any() )
                return;

            var relevantDatapoints = datapoints.Where(x => Datapoint.StockFinancials.HasFlag(x));
            var stockFinancialsMapper = new StockFinancialsMapper();

            foreach ( var security in list )
            {
                var stockFinancials = stockFinancialsRespository.Get(security.Ticker, relevantDatapoints);

                if ( stockFinancials is not null )
                    security.Map(stockFinancialsMapper.MapToSecurity(relevantDatapoints, stockFinancials));
            }
        }

        private SecuritiesList PullBaseSecurityList(IEnumerable<string> indices)
        {
            return stockIndicesRespository.Get(indices).Select(x => new Security() { Ticker = x }).ToSecurityList();
        }
    }

}
