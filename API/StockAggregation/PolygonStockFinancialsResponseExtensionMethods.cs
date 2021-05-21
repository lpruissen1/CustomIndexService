using ApiClient.Models;
using Core;
using StockScreener.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation
{
    public static class PolygonStockFinancialsResponseExtensionMethods
    {
        public static IEnumerable<QuarterlyStockFinancialsData> GetNewStockFinancialsEntries(this PolygonStockFinancialsResponse response, IEnumerable<Entry> dataEntry)
        {
            var mostRecent = dataEntry?.LastOrDefault().timestamp ?? 0;

            return response.Results.Where(entry => entry.ReportPeriod.ToUnix() > mostRecent);
        }
    }
}
