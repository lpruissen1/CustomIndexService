using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.Response
{
	public class AccountHistoryResponse
	{
		public int[] timestamp { get; set; }
		public decimal[] equity { get; set; }
		public double[] profit_loss { get; set; }
		public double[] profit_loss_pct { get; set; }
	}
}
