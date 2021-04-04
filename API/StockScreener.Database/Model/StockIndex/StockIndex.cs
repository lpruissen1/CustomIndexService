using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model.StockIndex
{
    public class StockIndex : DbEntity
    {
        public string Name;
        public List<string> Tickers = new List<string>();
    }
}
