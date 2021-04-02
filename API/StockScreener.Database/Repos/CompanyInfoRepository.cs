using Database.Core;
using MongoDB.Driver;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class CompanyInfoRepository : BaseRepository<CompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<CompanyInfo> Get(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
            var projection = Builders<CompanyInfo>.Projection.Include(x => x.Ticker).Include(x => x.Sector).Include(x => x.Industry);
            return dbCollection.Find(x => tickers.Contains(x.Ticker)).Project<CompanyInfo>(projection).ToEnumerable();
        }

        public CompanyInfo Get(string ticker, IEnumerable<BaseDatapoint> dataPoints)
        {
            var projection = Builders<CompanyInfo>.Projection.Include(x => x.Ticker).Include(x => x.Sector).Include(x => x.Industry);
            return dbCollection.Find(x => ticker == x.Ticker).Project<CompanyInfo>(projection).FirstOrDefault();
        }
    }
}

