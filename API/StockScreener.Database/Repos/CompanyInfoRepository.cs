using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
    public class CompanyInfoRepository : BaseRepository<CompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IMongoDBContext context) : base(context) { }

        public CompanyInfo Get(string ticker, IEnumerable<BaseDatapoint> dataPoints)
        {
            var projection = Builders<CompanyInfo>.Projection.Include(x => x.Ticker).Include(x => x.Sector).Include(x => x.Industry);
            return dbCollection.Find(x => ticker == x.Ticker).Project<CompanyInfo>(projection).FirstOrDefault() ?? new CompanyInfo { Ticker = ticker };
		}

		public override void Update(CompanyInfo info)
		{
			var filter = Builders<CompanyInfo>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<CompanyInfo, CompanyInfo>() { IsUpsert = true});
		}
	}
}

