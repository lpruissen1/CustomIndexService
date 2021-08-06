using Database.Core;
using System;
using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodEarnings
	{
		public Dictionary<DateTime, QuarterlyEarningsEntry> History { get; set; }
	}
}
