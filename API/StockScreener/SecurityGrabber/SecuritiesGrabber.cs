using StockScreener.Core;
using StockScreener.Database.Repos;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber.BaseDataMapper;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.SecurityGrabber
{
    public class SecuritiesGrabber : ISecuritiesGrabber
    {
        private readonly ICompanyInfoRepository companyInfoRespository;
        private readonly IStockFinancialsRepository stockFinancialsRespository;
        private readonly IStockIndexRepository stockIndicesRespository;
        private readonly IPriceDataRepository priceDataRepository;

        private SecuritiesList<BaseSecurity> list;

        public SecuritiesGrabber(IStockFinancialsRepository stockFinancialsRespository, ICompanyInfoRepository companyInfoRespository, IStockIndexRepository stockIndicesRespository, IPriceDataRepository priceDataRepository)
        {
            this.companyInfoRespository = companyInfoRespository;
            this.stockFinancialsRespository = stockFinancialsRespository;
            this.stockIndicesRespository = stockIndicesRespository;
            this.priceDataRepository = priceDataRepository;
        }

        public SecuritiesList<BaseSecurity> GetSecurities(SecuritiesSearchParams searchParams)
        {
            list = PullBaseSecurityList(searchParams.Indices);

            AddCompanyInfo(searchParams.Datapoints);
            AddStockFinancials(searchParams.Datapoints);

            return list;
        }

        private void AddCompanyInfo(IEnumerable<BaseDatapoint> datapoints)
        {
            if (!datapoints.Any())
                return;

            foreach (var security in list)
            {
                var companyInfo = companyInfoRespository.Get(security.Ticker, datapoints);
                // create mapper right here for company info
                if (companyInfo is not null)
                {
                    security.Industry = companyInfo.Industry;
                    security.Sector = companyInfo.Sector;
                }
            }
        }

        private void AddPrice(IEnumerable<BaseDatapoint> datapoints)
        {
            if (!datapoints.Any())
                return;

            foreach (var security in list)
            {
                var priceInfo = priceDataRepository.Get(security.Ticker);
                // create mapper right here for company info
                if (companyInfo is not null)
                {
                    security.Industry = companyInfo.Industry;
                    security.Sector = companyInfo.Sector;
                }
            }
        }

        private void AddStockFinancials(IEnumerable<BaseDatapoint> datapoints)
        {
            if (!datapoints.Any())
                return;

            var relevantDatapoints = datapoints.Where(x => BaseDatapoint.StockFinancials.HasFlag(x));
            var stockFinancialsMapper = new StockFinancialsMapper();

            foreach (var security in list)
            {
                var stockFinancials = stockFinancialsRespository.Get(security.Ticker, relevantDatapoints);

                if (stockFinancials is not null)
                    security.Map(stockFinancialsMapper.MapToSecurity(relevantDatapoints, stockFinancials));
            }
        }

        private SecuritiesList<BaseSecurity> PullBaseSecurityList(IEnumerable<string> indices)
        {
            return new SecuritiesList<BaseSecurity>(stockIndicesRespository.Get(indices).Select(x => new BaseSecurity() { Ticker = x }));
        }
    }

}
