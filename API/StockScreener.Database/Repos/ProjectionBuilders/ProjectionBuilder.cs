using Database.Core;
using MongoDB.Driver;
using StockScreener.Core;
using System;
using System.Collections.Generic;

namespace Database.Repositories
{
    public abstract class ProjectionBuilder<TDataType> where TDataType : StockDbEntity 
    {
        protected ProjectionDefinitionBuilder<TDataType> projection;
        protected Dictionary<BaseDatapoint, Action> datapointMapper;

        public ProjectionBuilder()
        {
            projection = Builders<TDataType>.Projection;
            projection.Include(x => x.Ticker);
        }

        public virtual ProjectionDefinition<TDataType> BuildProjection(IEnumerable<BaseDatapoint> datapoints) 
        {
            foreach(var datapoint in datapoints )
            {
                datapointMapper[datapoint].Invoke();
            }

            return projection.Combine();
        }
    }
}