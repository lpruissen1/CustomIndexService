using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class SecuritiesList 
    {
        private readonly List<Security> securities = new List<Security>();

        public SecuritiesList(List<Security> securities)
        {
            this.securities = securities;
        }

        public void Add(Security security)
        {
            securities.Add(security);
        }

        public SecuritiesList Where(Func<Security, bool> clause)
        {
            return new SecuritiesList(securities.Where(x => clause(x)).ToList());
        }

    }

}
