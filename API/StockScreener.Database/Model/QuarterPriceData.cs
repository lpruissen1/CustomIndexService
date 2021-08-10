using Database.Core;
using StockScreener.Database.Model.Price;
using System;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class QuarterPriceData : StockDbEntity
	{
		public DateTime Month { get; set; }
		public List<Candle> Days { get; set; } = new List<Candle>();
	}
}
