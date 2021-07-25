using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class EarningsHistory : StockDbEntity
	{
		public List<EarningsEntry> Entries { get; set; }
	}
}
