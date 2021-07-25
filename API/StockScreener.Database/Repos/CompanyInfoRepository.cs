using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Repos.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
	public class CompanyInfoRepository : BaseRepository<CompanyInfo>, ICompanyInfoRepository
    {
		private CompanyInfoProjectionBuilder CompanyInfoProjectionBuilder = new CompanyInfoProjectionBuilder();
        public CompanyInfoRepository(IMongoDBContext context) : base(context) { }

        public CompanyInfo Get(string ticker, IEnumerable<BaseDatapoint> dataPoints)
        {
			var projection = CompanyInfoProjectionBuilder.BuildProjection(dataPoints);
            return dbCollection.Find(x => ticker == x.Ticker).Project<CompanyInfo>(projection).FirstOrDefault() ?? new CompanyInfo { Ticker = ticker };
		}

        public IEnumerable<CompanyInfo> GetMany(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> dataPoints)
        {
			var projection = CompanyInfoProjectionBuilder.BuildProjection(dataPoints);
			var filter = Builders<CompanyInfo>.Filter.In(x => x.Ticker, tickers);
            return dbCollection.Find(filter).Project<CompanyInfo>(projection).ToEnumerable();
		}

        public IEnumerable<string> GetAllTickers()
        {
			var projection = CompanyInfoProjectionBuilder.BuildProjection(Enumerable.Repeat(BaseDatapoint.Ticker, 1));
            return dbCollection.Find((_ => true)).Project<CompanyInfo>(projection).ToEnumerable().Select(x => x.Ticker);
		}

		public override void Update(CompanyInfo info)
		{
			var filter = Builders<CompanyInfo>.Filter.Eq(e => e.Ticker, info.Ticker);

			dbCollection.FindOneAndReplace(filter, info, new FindOneAndReplaceOptions<CompanyInfo, CompanyInfo>() { IsUpsert = true});
		}
	}
}

