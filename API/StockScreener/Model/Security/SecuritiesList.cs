using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.BaseSecurity
{
    public class SecuritiesList<TSecurity> : IEnumerable<TSecurity> where TSecurity : Security
    {
        private List<TSecurity> securities = new List<TSecurity>();

        public SecuritiesList(IEnumerable<TSecurity> securities)
        {
            this.securities = securities.ToList();
        }

        public SecuritiesList()
        {
            this.securities = securities.ToList();
        }

        public void Add(TSecurity security)
        {
            securities.Add(security);
        }

        public void RemoveAll(Func<TSecurity, bool> clause)
        {
            securities.RemoveAll(x => clause(x));
        }

        public int Count => securities.Count;

        public TSecurity this[int index] => securities[index];

        public IEnumerator<TSecurity> GetEnumerator()
        {
            return securities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
