using Database.Model.StockData;
using Database.Repositories;
using StockScreener.Core;
using StockScreener.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class SecuritiesGrabber : ISecuritiesGrabber
    {
        private readonly Dictionary<Type, Datapoint> metricMapper;
        private readonly ICompanyInfoRepository companyInfoRespository;
        private readonly IStockIndexRepository stockIndicesRespository;

        public SecuritiesGrabber(ICompanyInfoRepository companyInfoRespository, IStockIndexRepository stockIndicesRespository)
        {
            metricMapper = new()
            {
                { typeof(SectorAndIndustryMetric), Datapoint.SectorAndIndustry }
            };

            this.companyInfoRespository = companyInfoRespository;
            this.stockIndicesRespository = stockIndicesRespository;
        }

        public SecuritiesList GetSecurities(MetricList metrics)
        {
            var datapoints = metrics.Select(metric => metricMapper[metric.GetType()]);
            var securityList = PullBaseSecurityList(metrics.MarketMetric);
            var companyInfo = PullCompanyInfo(datapoints);

            return securityList;
        }

        private IEnumerable<CompanyInfo> PullCompanyInfo(IEnumerable<Datapoint> datapoints)
        {
            return companyInfoRespository.Get(datapoints.Where(x => x.HasFlag(Datapoint.CompanyInfo)));
        }

        private SecuritiesList PullBaseSecurityList(MarketMetric market)
        {
            return stockIndicesRespository.Get(market.markets).Select(x => new Security() { Ticker = x }).ToSecurityList();
        }
    }
}
