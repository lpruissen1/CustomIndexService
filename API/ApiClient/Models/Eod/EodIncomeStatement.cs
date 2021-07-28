using Database.Core;
using System;
using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodIncomeStatement : StockDbEntity
	{
		public Dictionary<DateTime, QuarterlyIncomeStatementEntry> quarterly { get; set; }
	}
}

