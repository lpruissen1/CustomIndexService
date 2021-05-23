using Database.Core;
using System;

namespace Users.Database.Model
{
	public class PasswordList : DbEntity
	{
		public Guid UserId { get; set; }
		public string CurrentPassword { get; set; }
		public string OldPassword1 { get; set; }
		public string OldPassword2 { get; set; }
		public string OldPassword3 { get; set; }
	}
}
