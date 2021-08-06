using Database.Core;
using System;
using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodBalanceSheet
	{
		public Dictionary<DateTime, QuarterlyBalanceSheetEntry> quarterly { get; set; }
	}
}

