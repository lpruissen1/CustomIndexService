using Database.Core;
using StockScreener.Core;
using System;
using System.Collections.Generic;

namespace StockScreener
{
    public abstract class SecurityMapper<TDbEntity> where TDbEntity : StockDbEntity
    {
        protected Dictionary<Datapoint, Action<TDbEntity>> blah;
        protected Security security;

        public SecurityMapper()
        {
        }

        public virtual Security MapToSecurity(IEnumerable<Datapoint> datapoints, TDbEntity stockFinancials)
        {
            security = new Security();

            foreach ( var datapoint in datapoints )
            {
                blah[datapoint].Invoke(stockFinancials);
            }

            return security;
        }
    }

}
