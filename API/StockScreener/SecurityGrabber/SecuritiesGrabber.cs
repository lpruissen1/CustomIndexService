﻿using Core;
using StockScreener.Core;
using StockScreener.Database.Model;
using StockScreener.Database.Repos.Interfaces;
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
        private readonly IMonthPriceDataRepository priceDataRepository;

        private SecuritiesList<BaseSecurity> list;

        public SecuritiesGrabber(IStockFinancialsRepository stockFinancialsRespository, ICompanyInfoRepository companyInfoRespository, IStockIndexRepository stockIndicesRespository, IMonthPriceDataRepository priceDataRepository)
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
            AddStockFinancials(tickers, searchParams.Datapoints);

			AddPrice(tickers, searchParams.PriceTimePeriod);

            return list;
		}

		public SecuritiesList<BaseSecurity> GetSecuritiesByTicker(List<string> tickers, IEnumerable<BaseDatapoint> datapoints)
		{
			list = new SecuritiesList<BaseSecurity>(tickers.Select(ticker => new BaseSecurity { Ticker = ticker}));

			AddCompanyInfo(tickers, datapoints);
			AddStockFinancials(tickers, datapoints);
			if(datapoints.Contains(BaseDatapoint.Price))
				AddPrice(tickers, TimePeriod.Year);

			return list;
		}

		private void AddCompanyInfo(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
			var relevantDatapoints = datapoints.Where(x => BaseDatapoint.CompanyInfo.HasFlag(x)).ToList();

			relevantDatapoints.Add(BaseDatapoint.Name);
			relevantDatapoints.Add(BaseDatapoint.Sector);

            var companyInfoMapper = new CompanyInfoMapper();

			var companyInfos = companyInfoRespository.GetMany(tickers, relevantDatapoints).ToList();

			foreach (var security in list)
            {
				var companyInfo = companyInfos.FirstOrDefault(x => x.Ticker == security.Ticker);

                if (companyInfo is not null)
                    security.Map(companyInfoMapper.MapToSecurity(relevantDatapoints, companyInfo));
            }
        }

        private void AddPrice(IEnumerable<string> tickers, TimePeriod timePeriod)
        {
			var priceInfos = priceDataRepository.GetPriceData(tickers, timePeriod).ToList();

            foreach (var security in list)
            {
				var priceInfo = priceInfos.First(x => x.Key == security.Ticker).Value;

                if (priceInfo is not null)
                {
					security.DailyPrice = MapMonthlyEntry(priceInfo).ToList();
                }
            }
        }

		private IEnumerable<PriceEntry> MapMonthlyEntry(IEnumerable<MonthPriceData> monthPriceData)
		{
			foreach(var month in monthPriceData)
			{
				foreach(var day in month.Days)
				{
					yield return new PriceEntry { Price = day.closePrice, Timestamp = day.timestamp };
				}
			}
		}

		private void AddStockFinancials(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
            var relevantDatapoints = datapoints.Where(x => BaseDatapoint.StockFinancials.HasFlag(x)).ToList();

			relevantDatapoints.Add(BaseDatapoint.MarketCap);

            var stockFinancialsMapper = new StockFinancialsMapper();

            var stockFinancials = stockFinancialsRespository.GetMany(tickers, relevantDatapoints).ToList();
            
			foreach (var security in list)
			{
				var stockFinancial = stockFinancials.FirstOrDefault(x => x.Ticker == security.Ticker);

				if (stockFinancial is not null)
                    security.Map(stockFinancialsMapper.MapToSecurity(relevantDatapoints, stockFinancial));
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
