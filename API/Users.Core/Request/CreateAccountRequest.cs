using Core;

namespace Users.Core.Request
{
	public class CreateAccountRequest
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TaxIdNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string DateOfBirth { get; set; }
		public string CountryOfTaxResidency { get; set; }
		public FundingSourceValue FundingSource { get; set; }
		public bool IsControlledPerson { get; set; }
		public bool IsAffiliatedExchangeOrFinra { get; set; }
		public bool IsPoliticallyExposed { get; set; }
		public bool ImmediateFamilyExposed { get; set; }
		public string PhotoIdFront { get; set; }
		public string PhotoIdBack { get; set; }
		public string IpAddress { get; set; }
		public double CustomerAndAccountAgreementSignedAt { get; set; }
	}
}
