using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class CashFlowHistory : StockDbEntity
	{
		public List<CashFlowEntry> Entries { get; set; }
	}
}
