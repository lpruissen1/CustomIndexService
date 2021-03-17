using Database.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreener.Database.Model.CompanyInfo
{
    public class CompanyInfo : DbEntity
    {
        public string Ticker;
        public string Name;
        public string Industry;
        public string Sector;
    }
}
