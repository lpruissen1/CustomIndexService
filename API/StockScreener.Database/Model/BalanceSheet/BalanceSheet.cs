using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model.BalanceSheet
{
	public class BalanceSheet : StockDbEntity
	{
		List<BalanceSheetEntry> Entries { get; set; }
	}
}
