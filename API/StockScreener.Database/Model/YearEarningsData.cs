using Database.Core;
using System;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class YearEarningsData : StockDbEntity
	{
		public DateTime Year { get; set; }
		public List<EarningsEntry> Quarters { get; set; } = new List<EarningsEntry>();
	}
}
