using System.Collections.Generic;

namespace Users.Core.Response
{
	public class AccountHistoryResponse
	{
		public Dictionary<int, decimal> AccountHistory { get; } = new Dictionary<int, decimal>();
		public Dictionary<int, decimal> NetContributions { get; } = new Dictionary<int, decimal>();
	}
}
