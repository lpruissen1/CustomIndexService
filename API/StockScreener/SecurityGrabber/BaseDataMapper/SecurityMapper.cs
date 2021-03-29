using Database.Core;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;

namespace StockScreener.SecurityGrabber.BaseDataMapper
{
    public abstract class SecurityMapper<TDbEntity> where TDbEntity : StockDbEntity
    {
        protected Dictionary<BaseDatapoint, Action<TDbEntity>> datapointMapperDictionary;
        protected BaseSecurity security;

        public SecurityMapper()
        {
        }

        public virtual BaseSecurity MapToSecurity(IEnumerable<BaseDatapoint> datapoints, TDbEntity stockFinancials)
        {
            security = new BaseSecurity();

            foreach (var datapoint in datapoints)
            {
                datapointMapperDictionary[datapoint].Invoke(stockFinancials);
            }

            return security;
        }
    }
}
