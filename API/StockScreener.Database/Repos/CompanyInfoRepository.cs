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
        private ProjectionDefinitionBuilder<CompanyInfo> filterBuilder;
        public CompanyInfoRepository(IMongoDBContext context) : base(context) {
            filterBuilder = Builders<CompanyInfo>.Projection;
        }

        public IEnumerable<CompanyInfo> Get(IEnumerable<string> tickers, IEnumerable<Datapoint> datapoints)
        {
            var projection = Builders<CompanyInfo>.Projection.Include(x => x.Ticker).Include(x => x.Sector).Include(x => x.Industry);
            return _dbCollection.Find(x => tickers.Contains(x.Ticker)).Project<CompanyInfo>(projection).ToEnumerable();
        }

        private ProjectionDefinition<CompanyInfo> AddSector()
        {
            return filterBuilder.Include(x => x.Sector);
        }
    }
}

