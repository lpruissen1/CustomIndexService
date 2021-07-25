using Database.Core;

namespace StockScreener.Database.Model.CompanyInfo
{
	public class CompanyInfo : StockDbEntity
    {
		public string Cusip;
        public string Name;
        public string Industry;
        public string Sector;
        public string Description;
        public string[] Indices;

        public bool isDelisted;

        public double LastUpdated;
    }
}
