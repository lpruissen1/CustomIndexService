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

        public SecuritiesGrabber(IStockFinancialsRepository stockFinancialsRespository, ICompanyInfoRepository companyInfoRespository, IStockIndexRepository stockIndicesRespository)
        {
            this.companyInfoRespository = companyInfoRespository;
            this.stockFinancialsRespository = stockFinancialsRespository;
            this.stockIndicesRespository = stockIndicesRespository;
        }

        public SecuritiesList GetSecurities(SecuritiesSearchParams searchParams)
        {
            var securityList = PullBaseSecurityList(searchParams.Indices);

            AddCompanyInfo(ref securityList, searchParams.Datapoints.Where(x => x.HasFlag(Datapoint.CompanyInfo)));
            AddStockFinancials(ref securityList, searchParams.Datapoints.Where(x => x.HasFlag(Datapoint.StockFinancials)));

            return securityList;
        }

        private void AddCompanyInfo(ref SecuritiesList list, IEnumerable<Datapoint> datapoints)
        {
            if ( datapoints.Count() == 0 )
                return;
           var companyInfo = companyInfoRespository.Get(list.Select(x => x.Ticker), datapoints);
           foreach(var security in list)
           {
                var info = companyInfo.FirstOrDefault(x => x.Ticker == security.Ticker);
                security.Industry = info.Industry;
                security.Sector = info.Sector;
           }
        }

        private void AddStockFinancials(ref SecuritiesList list, IEnumerable<Datapoint> datapoints)
        {
            if ( datapoints.Count() == 0 )
                return;

            var stockFinancials = stockFinancialsRespository.Get(list.Select(x => x.Ticker), datapoints);
            foreach(var security in list)
            {
                var info = stockFinancials.FirstOrDefault(x => x.Ticker == security.Ticker);
                security.MarketCap = info.MarketCap[0].marketCap;
            }
        }

        private SecuritiesList PullBaseSecurityList(IEnumerable<string> indices)
        {
            return stockIndicesRespository.Get(indices).Select(x => new Security() { Ticker = x }).ToSecurityList();
        }
    }
}
