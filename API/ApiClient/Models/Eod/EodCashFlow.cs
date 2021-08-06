using Database.Core;
using System;
using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodCashFlow
	{
		public Dictionary<DateTime, QuarterlyCashFlowEntry> quarterly { get; set; }
	}
}

