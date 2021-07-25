
namespace Users.Core.Request
{
	public class UpgradeUserRequest
	{
		public string UserId { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string DateOfBirth { get; set; }
		public string CountryOfTaxResidency { get; set; }
		public FundingSource FundingSource { get; set; }
		public bool IsControlledPerson { get; set; }
		public bool IsAffiliatedExchangeOrFinra { get; set; }
		public bool IsPoliticallyExposed { get; set; }
		public bool ImmediateFamilyExposed { get; set; }
	}
}
