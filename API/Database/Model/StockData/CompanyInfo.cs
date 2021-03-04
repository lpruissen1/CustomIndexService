using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.StockData
{
    public class CompanyInfo : DbEntity
    {
        public string Ticker { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }

        public override string GetPrimaryKey()
        {
            return Id;
        }
    }

//	dbInfo DBEntity
//	CompanyName string
//Ticker               string
//Sector               string
//Industry             string
//GrowthRate           float32
//MarketCap            float32
//EarningsPerShare     float32
//PriceToEarningsRatio float32
//Beta                 float32
//DividendYield        float32
//Indices[]string
//Timestamp            string
}
