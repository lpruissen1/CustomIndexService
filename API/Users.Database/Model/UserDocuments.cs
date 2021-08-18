using Database.Core;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class UserDocuments : DbEntity
	{
		public string UserId { get; set; }
		public List<Document> Documents { get; set; } = new List<Document>();
	}
}
