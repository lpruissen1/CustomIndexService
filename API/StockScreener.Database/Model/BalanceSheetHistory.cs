using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class BalanceSheetHistory : StockDbEntity
	{
		public List<BalanceSheetEntry> Entries { get; set; }
	}
}
