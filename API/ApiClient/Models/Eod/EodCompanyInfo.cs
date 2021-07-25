using System;

namespace ApiClient.Models.Eod
{
	public class EodCompanyInfo
	{
		public string Ticker { get; set; }
		public string Name { get; set; }
		public string Cusip { get; set; }
		public string Description { get; set; }
		public string Sector { get; set; }
		public string Industry { get; set; }
		public bool isDelisted { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
