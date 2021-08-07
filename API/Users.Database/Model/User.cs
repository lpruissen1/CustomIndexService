using Database.Core;
using System;
using System.Collections.Generic;
using Users.Core;

namespace Users.Database.Model
{
	public class User : DbEntity
	{
		public string UserId { get; set; }
		public string AccountType { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string DateOfBirth { get; set; }
		public string CountryOfTaxResidency { get; set; }
	}

	public class UserDisclosures : DbEntity
	{
		public string UserId { get; set; }
		public FundingSourceValue FundingSource { get; set; }
		public bool IsControlledPerson { get; set; }
		public bool IsAffiliatedExchangeOrFinra { get; set; }
		public bool IsPoliticallyExposed { get; set; }
		public bool ImmediateFamilyExposed { get; set; }
	}

	public class UserAccounts : DbEntity
	{
		public string UserId { get; set; }
		public List<Account> Accounts { get; set; }
	}

	public class Account
	{
		public string AccountId { get; set; }
		public string AccountName { get; set; }
		public string Institution { get; set; }
		public DateTime DateRegistered { get; set; }
		public DateTime DateCreated { get; set; }

	}
}
