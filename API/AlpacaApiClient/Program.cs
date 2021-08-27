using AlpacaApiClient.Model;
using AlpacaApiClient.Model.Request;
using System;
using Users.Core;

namespace AlpacaApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			var request = new AlpacaCreateAccountRequest()
			{
				contact = new AlpacaAccountContact()
				{
					email_address = "alex@example.com",
					phone_number = "+1555-666-7788",
					street_address = new[] { "20 N San Mateo Dr" },
					city = "San Mateo",
					state = "CA"
				},
				identity = new AlpacaAccountIdentity()
				{
					given_name = "John",
					family_name = "Doe",
					date_of_birth = "1990-01-01",
					tax_id = "666-55-4321",
					tax_id_type = TaxIdTypeValue.USA_SSN,
					country_of_tax_residence = "USA",
					funding_source = new[] { FundingSourceValue.employment_income }
				},
				disclosures = new AlpacaAccountDisclosures
				{
					is_control_person = false,
					is_affiliated_exchange_or_finra = false,
					is_politically_exposed = false,
					immediate_family_exposed = false
				},
				agreements = new[] {
					new AlpacaAccountAgreement
					{
						agreement = AggrementTypeValue.margin_agreement,
						signed_at = "2020-09-11T18:13:44Z",
						ip_address = "185.13.21.99"
					},
					new AlpacaAccountAgreement
					{
						agreement = AggrementTypeValue.account_agreement,
						signed_at = "2020-09-11T18:13:44Z",
						ip_address = "185.13.21.99"
					},
					new AlpacaAccountAgreement
					{
						agreement = AggrementTypeValue.customer_agreement,
						signed_at = "2020-09-11T18:13:44Z",
						ip_address = "185.13.21.99"
					}
				},
				documents = new[] {
					new AlpacaAccountDocument
					{
						document_type = DocumentTypeValue.identity_verification,
						content = "RFerjiUxFUTXLQ7pMEckpWYjLPviLRZdrc4=",
						mime_type = "application/pdf"
					}
				}
			};

			var createRelationship = new AlpacaAchRelationshipRequest()
			{
				account_owner_name = "Jeff Bezos",
				nickname = "My Billion dollar bank account",
				bank_account_number = "32131231abc",
				bank_routing_number = "121000358",
				bank_account_type = "CHECKING"
			};

			var alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" });

        }
    }
}
