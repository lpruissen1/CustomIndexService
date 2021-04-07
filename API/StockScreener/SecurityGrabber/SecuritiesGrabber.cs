using StockScreener.Core;
using StockScreener.Database.Model.Price;
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
            AddPrice(searchParams.Datapoints);
            return list;
        }

        public SecuritiesList<BaseSecurity> GetSecurities(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
            list = new SecuritiesList<BaseSecurity>(tickers.Select(ticker => new BaseSecurity() { Ticker = ticker}));

            AddCompanyInfo(datapoints);
            AddStockFinancials(datapoints);
            AddPrice(datapoints);

            return list;
        }

        private void AddCompanyInfo(IEnumerable<BaseDatapoint> datapoints)
        {
            if (!datapoints.Any())
                return;

            var relevantDatapoints = datapoints.Where(x => BaseDatapoint.CompanyInfo.HasFlag(x));
            var companyInfoMapper = new CompanyInfoMapper();

            foreach (var security in list)
            {
                var companyInfo = companyInfoRespository.Get(security.Ticker, relevantDatapoints);

                if (companyInfo is not null)
                    security.Map(companyInfoMapper.MapToSecurity(relevantDatapoints, companyInfo));
            }
        }

        private void AddPrice(IEnumerable<BaseDatapoint> datapoints)
        {
            if (!datapoints.Any(x => x == BaseDatapoint.Price))
                return;

            foreach (var security in list)
            {
                var priceInfo = priceDataRepository.GetPriceData<DayPriceData>(security.Ticker);

                if (priceInfo is not null)
                {
                    security.DailyPrice = new List<PriceEntry>(priceInfo.Select(x => new PriceEntry { Price = x.closePrice, Timestamp = x.timestamp }));
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
