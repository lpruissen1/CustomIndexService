using Database.Core;
using System;
using System.Collections.Generic;

namespace ApiClient.Models.Eod.Earnings
{
	public class EodEarnings : StockDbEntity
	{
		public Dictionary<DateTime, QuarterlyEarningsEntry> History { get; set; }
	}
}
