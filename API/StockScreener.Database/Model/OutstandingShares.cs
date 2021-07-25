using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class OutstandingSharesHistory : StockDbEntity
	{
		public List<OutstandingSharesEntry> Entries { get; set; }
	}
}
