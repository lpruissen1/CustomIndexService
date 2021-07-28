using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class IncomeStatementHistory : StockDbEntity
	{
		public List<IncomeStatementEntry> Entries { get; set; }
	}
}
