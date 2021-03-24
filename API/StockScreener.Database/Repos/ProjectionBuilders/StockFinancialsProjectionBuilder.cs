using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;

namespace Database.Repositories
{
    public class StockFinancialsProjectionBuilder : ProjectionBuilder<StockFinancials>
    {
        public StockFinancialsProjectionBuilder() : base()
        {
            datapointMapper = new Dictionary<Datapoint, Action>()
            {
                {Datapoint.Revenue, AddRevenue }
            };
        }

        private void AddRevenue()
        {
            projection.Include(x => x.Revenues);
        }
    }
}