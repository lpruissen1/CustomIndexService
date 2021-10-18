using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.Response
{
	public class AccountHistoryResponse
	{
		public Dictionary<int, decimal> AccountHistory { get; } = new Dictionary<int, decimal>();
	}
}
