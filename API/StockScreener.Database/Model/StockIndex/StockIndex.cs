using Database.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreener.Database.Model.StockIndex
{
    public class StockIndex : DbEntity
    {
        public string Name;
        public string[] Tickers;
    }
}
