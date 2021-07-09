using StockScreener.Core;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Repos;
using StockScreener.Interfaces;
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

        public SecuritiesList<BaseSecurity> GetSecuritiesByIndex(SecuritiesSearchParams searchParams)
        {
			var tickers = PullSecurityByIndex(searchParams.Markets);
            list = CreateBaseSecurityList(tickers);

			AddCompanyInfo(tickers, searchParams.Datapoints);
            AddStockFinancials( searchParams.Datapoints);
            AddPrice( searchParams.Datapoints);

            return list;
		}

		public SecuritiesList<BaseSecurity> GetSecuritiesByTicker(List<string> tickers, IEnumerable<BaseDatapoint> datapoints)
		{
			list = new SecuritiesList<BaseSecurity>(tickers.Select(ticker => new BaseSecurity { Ticker = ticker}));

			AddCompanyInfo(tickers, datapoints);
			AddStockFinancials(datapoints);
			AddPrice(datapoints);

			return list;
		}

		private void AddCompanyInfo(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
			var relevantDatapoints = datapoints.Where(x => BaseDatapoint.CompanyInfo.HasFlag(x));

			if (!relevantDatapoints.Any())
                return;

            var companyInfoMapper = new CompanyInfoMapper();

			var companyInfos = companyInfoRespository.GetMany(tickers, relevantDatapoints);

			foreach (var security in list)
            {
				var companyInfo = companyInfos.First(x => x.Ticker == security.Ticker);

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

        private SecuritiesList<BaseSecurity> CreateBaseSecurityList(IEnumerable<string> tickers)
        {
            return new SecuritiesList<BaseSecurity>(tickers.Select(ticker => new BaseSecurity() { Ticker = ticker }));
        }

        private IEnumerable<string> PullSecurityByIndex(IEnumerable<string> indices)
        {
            return stockIndicesRespository.Get(indices);
        }
    }

}
