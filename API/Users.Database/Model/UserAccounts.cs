using Database.Core;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserAccounts : DbEntity
	{
		public string UserId { get; set; }
		public List<Account> Accounts { get; set; } = new List<Account>();
	}
}
