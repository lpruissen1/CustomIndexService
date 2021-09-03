using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Users.Database.Model
{
	public class UserDisclosures : DbEntity
	{
		[BsonRepresentation(BsonType.String)]
		public Guid UserId { get; set; }
		public FundingSourceValue FundingSource { get; set; }
		public bool IsControlledPerson { get; set; }
		public bool IsAffiliatedExchangeOrFinra { get; set; }
		public bool IsPoliticallyExposed { get; set; }
		public bool ImmediateFamilyExposed { get; set; }
	}
}
