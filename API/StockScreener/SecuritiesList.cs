using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener
{
    public class SecuritiesList : List<Security>
    {
        //private List<Security> securities = new List<Security>();

        //public SecuritiesList(List<Security> securities)
        //{
        //    this.securities = securities;
        //}

        //public void Add(Security security)
        //{
        //    securities.Add(security);
        //}

        //public SecuritiesList Where(Func<Security, bool> clause)
        //{
        //    securities = securities.Where(x => clause(x)).ToList();
        //    return this;
        //}

        //public int Count()
        //{
        //    return securities.Count;
        //}

        //public bool IsEmpty()
        //{
        //    return securities.Count == 0;
        //}
        
        //public static SecuritiesList Empty()
        //{
        //    return new SecuritiesList(null);
        //}

    }

}
