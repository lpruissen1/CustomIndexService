using AggregationService.Core;
using ApiClient.Models;
using StockScreener.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AggregationService
{
    public static class PolygonStockFinancialsResponseExtensionMethods
    {
        public static IEnumerable<QuarterlyStockFinancialsData> GetNewStockFinancialsEntries(this PolygonStockFinancialsResponse response, IEnumerable<Entry> dataEntry)
        {
            var mostRecent = dataEntry.LastOrDefault()?.timestamp ?? (double)DateTime.UtcNow.ToUnix();

            return response.Results.Where(entry => entry.ReportPeriod.ToUnix() > mostRecent);
        }
    }
}
