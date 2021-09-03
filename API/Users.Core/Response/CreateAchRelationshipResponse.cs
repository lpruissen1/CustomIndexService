using System;

namespace Users.Core.Response
{
	public class CreateAchRelationshipResponse
	{
		public string Status { get; set; }
		public string Nickname { get; set; }
	}
	public class GetAchRelationshipResponse
	{
		public string Status { get; set; }
		public string Nickname { get; set; }
		public Guid RelationshipId { get; set; }
	}
}
