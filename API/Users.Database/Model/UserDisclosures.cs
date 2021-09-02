﻿using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Users.Core;

namespace Users.Database.Model
{
	public class UserDisclosures : DbEntity
	{
		public string UserId { get; set; }
		[BsonRepresentation(BsonType.String)]
		public FundingSourceValue FundingSource { get; set; }
		public bool IsControlledPerson { get; set; }
		public bool IsAffiliatedExchangeOrFinra { get; set; }
		public bool IsPoliticallyExposed { get; set; }
		public bool ImmediateFamilyExposed { get; set; }
	}
}
