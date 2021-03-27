using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model.Price
{
    public class PriceData : StockDbEntity
	{
		public PriceData()
		{
			Candle = new List<Candle>();
		}

		public List<Candle> Candle { get; init; }

		public int Version = 1;
	}
}
