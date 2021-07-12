using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System;
using System.Collections.Generic;

namespace Database.Repositories
{
	public class CompanyInfoProjectionBuilder : ProjectionBuilder<CompanyInfo>
    {
        public CompanyInfoProjectionBuilder() : base()
        {
            datapointMapper = new Dictionary<BaseDatapoint, Action>()
            {
                {BaseDatapoint.Industry, AddIndustry },
                {BaseDatapoint.Sector, AddSector },
                {BaseDatapoint.Ticker, AddTicker }
            };
        }

        private void AddSector()
        {
            projection.Include(x => x.Sector);
        }

        private void AddIndustry()
        {
            projection.Include(x => x.Industry);
        }

        private void AddTicker()
        {
            projection.Include(x => x.Ticker);
        }
    }
}
