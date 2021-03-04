using Database.Model.StockData;
using MongoDB.Bson;
using MongoDB.Driver;
using StockScreener.Core;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class CompanyInfoRepository : BaseRepository<CompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<CompanyInfo> Get(IEnumerable<Datapoint> datapoints)
        {
            var projection = Builders<CompanyInfo>.Projection.Include(x => x.Sector).Include(x => x.Industry);
            return _dbCollection.Find(_ => true).Project<CompanyInfo>(projection).ToEnumerable();
        }
    }
}

