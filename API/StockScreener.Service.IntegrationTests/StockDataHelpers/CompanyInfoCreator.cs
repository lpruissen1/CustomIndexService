using StockScreener.Database.Model.CompanyInfo;

namespace StockScreener.Service.IntegrationTests.StockDataHelpers
{
    public static class CompanyInfoCreator
    {
        public static CompanyInfo GetCompanyInfo(string ticker)
        {
            return new CompanyInfo { Ticker = ticker };
        }

        public static CompanyInfo AddIndustry(this CompanyInfo companyInfo, string industry)
        {
            companyInfo.Industry = industry;
            return companyInfo;
        }

        public static CompanyInfo AddSector(this CompanyInfo companyInfo, string sector)
        {
            companyInfo.Sector = sector;
            return companyInfo;
        }
    }
}
