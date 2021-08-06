using Database.Core;
using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodOutstandingShares
	{
		public Dictionary<string, QuarterlyOutstandingSharesEntry> quarterly { get; set; }
	}
}

