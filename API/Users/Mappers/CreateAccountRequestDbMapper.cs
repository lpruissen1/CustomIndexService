using AlpacaApiClient.Model;
using AlpacaApiClient.Model.Response;
using System;
using Users.Core.Request;
using Users.Database.Model;

namespace Users.Mappers
{
	public static class CreateAccountRequestDbMapper
	{
		public static User MapToUser(User user, CreateAccountRequest createAccountRequest)
		{
			var (suffix, hash) = HashTaxId(createAccountRequest.TaxIdNumber);

			user.PhoneNumber = createAccountRequest.PhoneNumber;
			user.StreetAddress = createAccountRequest.StreetAddress;
			user.City = createAccountRequest.City;
			user.State = createAccountRequest.State;
			user.PostalCode = createAccountRequest.PostalCode;
			user.DateOfBirth = createAccountRequest.DateOfBirth;
			user.CountryOfTaxResidency = createAccountRequest.CountryOfTaxResidency;
			user.TaxIdHash = hash;
			user.TaxIdSuffix = suffix;

			return user;
		}

		public static UserAccounts MapUserAccounts(CreateAccountRequest createAccountRequest, AlpacaCreateAccountResponse alpacaResponse)
		{
			var userAccounts = new UserAccounts() { UserId = createAccountRequest.UserId };

			userAccounts.Accounts.Add(new Account()
			{
				AccountId = alpacaResponse.id,
				AccountDisplayId = alpacaResponse.account_number,
				Active = alpacaResponse.status == AlpacaAccountStatusValue.APPROVED,
				DateRegistered = DateTime.Now,
				Institution = "Alpaca Markets",
			});

			return userAccounts;
		}

		public static UserDisclosures MapUserDisclosures(CreateAccountRequest createAccountRequest)
		{
			var userAccounts = new UserDisclosures() 
			{ 
				UserId = createAccountRequest.UserId,
				FundingSource = createAccountRequest.FundingSource,
				IsControlledPerson = createAccountRequest.IsControlledPerson,
				IsAffiliatedExchangeOrFinra = createAccountRequest.IsAffiliatedExchangeOrFinra,
				IsPoliticallyExposed = createAccountRequest.IsPoliticallyExposed,
				ImmediateFamilyExposed = createAccountRequest.ImmediateFamilyExposed
			};

			return userAccounts;
		}

		public static UserDocuments MapUserDocuments(CreateAccountRequest createAccountRequest)
		{
			var userDocuments = new UserDocuments() { UserId = createAccountRequest.UserId };

			userDocuments.Documents.Add(new Document()
			{
				document_type = DocumentTypeValue.identity_verification,
				document_sub_type = "photoId_front",
				content = createAccountRequest.PhotoIdFront,
				mime_type = "image/jpeg"
			});

			userDocuments.Documents.Add(new Document()
			{
				document_type = DocumentTypeValue.identity_verification,
				document_sub_type = "photoId_back",
				content = createAccountRequest.PhotoIdBack,
				mime_type = "image/jpeg"

			});

			return userDocuments;
		}

		private static (string suffix, string hash) HashTaxId(string taxId)
		{
			taxId = taxId.Replace("-", "");

			if (taxId.Length != 9)
				throw new Exception("Invalid TaxId Length");

			var suffix = taxId.Substring(5, 4);
			var hash = new BCryptHasher().Hash(taxId.Substring(0, 5));

			return (suffix, hash);
		}
	}
}
