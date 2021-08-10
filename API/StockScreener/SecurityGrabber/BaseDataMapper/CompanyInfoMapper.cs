using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System;
using System.Collections.Generic;

namespace StockScreener.SecurityGrabber.BaseDataMapper
{
    public class CompanyInfoMapper : SecurityMapper<CompanyInfo>
    {
        public CompanyInfoMapper()
        {
            datapointMapperDictionary = new Dictionary<BaseDatapoint, Action<CompanyInfo>>()
            {
                {BaseDatapoint.Sector, AddSector },
                {BaseDatapoint.Industry, AddIndustry },
                {BaseDatapoint.Name, AddName},
            };
        }

        private void AddSector(CompanyInfo companyInfo)
        {
            security.Sector = companyInfo.Sector;
        }

        private void AddIndustry(CompanyInfo companyInfo)
        {
            security.Industry = companyInfo.Industry;
        }

        private void AddName(CompanyInfo companyInfo)
        {
            security.Name = companyInfo.Name;
        }
    }


}
