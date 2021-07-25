using StockScreener.Database.Model;
using System;

namespace ApiClient.Models.Eod
{
	public class QuarterlyOutstandingSharesEntry : Entry
	{
		public DateTime dateFormatted { get; set; }
		public double shares { get; set; }
	}
}
