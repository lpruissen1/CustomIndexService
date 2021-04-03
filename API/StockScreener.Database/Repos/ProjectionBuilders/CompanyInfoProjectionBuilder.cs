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
            datapointMapper = new Dictionary<Datapoint, Action>()
            {
                {Datapoint.CompanyInfo, AddIndustry },
                {Datapoint.Sector, AddSector }
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
    }
}