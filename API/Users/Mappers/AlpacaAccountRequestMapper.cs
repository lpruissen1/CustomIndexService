﻿using AlpacaApiClient.Model;
using AlpacaApiClient.Model.Request;
using Core;
using System.Collections.Generic;
using Users.Core.Request;

namespace Users.Mappers
{
	public static class AlpacaAccountRequestMapper
	{
		public static AlpacaCreateAccountRequest MapCreateAccountRequest(CreateAccountRequest createAccountRequest)
		{
			var request = new AlpacaCreateAccountRequest();

			request.contact = MapAccountContactInfo(createAccountRequest);
			request.identity = MapAccountIdentity(createAccountRequest);
			request.disclosures = MapAccountDisclosures(createAccountRequest);
			request.agreements = MapAccountAggrements(createAccountRequest);
			request.documents = MapAccountDocuments(createAccountRequest);

			return request;
		}
		public static AlpacaAchRelationshipRequest MapCreateAchRelationshipRequest(CreateAchRelationshipRequest createAccountRequest)
		{
			var request = new AlpacaAchRelationshipRequest();

			request.account_owner_name = createAccountRequest.BankAccountOwnerName;
			request.bank_account_number = createAccountRequest.BankAccountNumber;
			request.bank_account_type = createAccountRequest.AccountType.ToString();
			request.bank_routing_number = createAccountRequest.BankAccountRoutingNumber;
			request.nickname = createAccountRequest.BankAccountNickname;

			return request;
		}

		public static AlpacaTransferRequest MapTransferRequest(FundAccountRequest fundAccountRequest)
		{
			var request = new AlpacaTransferRequest();

			request.transfer_type = fundAccountRequest.TransferType;
			request.relationship_id = fundAccountRequest.RelationshipId;
			request.amount = fundAccountRequest.Amount;
			request.direction = fundAccountRequest.Direction;

			return request;
		}

		public static IEnumerable<AlpacaMarketOrderRequest> MapBulkRequestOrder(BulkOrderRequest bulkOrderRequest)
		{
			foreach (var order in bulkOrderRequest.Orders)
			{
				yield return new AlpacaMarketOrderRequest()
				{
					symbol = order.Ticker,
					notional = order.Amount,
					side = bulkOrderRequest.Direction,
					type = OrderType.market,
					time_in_force = OrderExecutionTimeframeValue.day
				};
			};
		}

		private static AlpacaAccountContact MapAccountContactInfo(CreateAccountRequest createAccountRequest)
		{
			return new AlpacaAccountContact()
			{
				email_address = createAccountRequest.EmailAddress,
				phone_number = createAccountRequest.PhoneNumber,
				street_address = new[] { createAccountRequest.StreetAddress },
				city = createAccountRequest.City,
				state = createAccountRequest.State
			};
		}

		private static AlpacaAccountIdentity MapAccountIdentity(CreateAccountRequest createAccountRequest)
		{
			return new AlpacaAccountIdentity()
			{
				given_name = createAccountRequest.FirstName,
				family_name = createAccountRequest.LastName,
				date_of_birth = createAccountRequest.DateOfBirth,
				tax_id = createAccountRequest.TaxIdNumber,
				tax_id_type = TaxIdTypeValue.USA_SSN,
				country_of_tax_residence = "USA",
				funding_source = new[] { createAccountRequest.FundingSource }
			};
		}

		private static AlpacaAccountDisclosures MapAccountDisclosures(CreateAccountRequest createAccountRequest)
		{
			return new AlpacaAccountDisclosures()
			{
				is_control_person = createAccountRequest.IsControlledPerson,
				is_affiliated_exchange_or_finra = createAccountRequest.IsAffiliatedExchangeOrFinra,
				is_politically_exposed = createAccountRequest.IsPoliticallyExposed,
				immediate_family_exposed = createAccountRequest.ImmediateFamilyExposed
			};
		}

		private static AlpacaAccountDocument[] MapAccountDocuments(CreateAccountRequest createAccountRequest)
		{
			var frontIdResponse = EncodedPictureParser.Parse(createAccountRequest.PhotoIdFront);
			var backIdResponse = EncodedPictureParser.Parse(createAccountRequest.PhotoIdBack);

			return new[] {
				new AlpacaAccountDocument()
				{
					document_type = DocumentTypeValue.identity_verification,
					document_sub_type = "idFront",
					content = frontIdResponse.Content,
					mime_type = frontIdResponse.MimeType
				},
				new AlpacaAccountDocument()
				{
					document_type = DocumentTypeValue.identity_verification,
					document_sub_type = "idBack",
					content = backIdResponse.Content,
					mime_type = backIdResponse.MimeType
				}
			};
		}

		private static AlpacaAccountAgreement[] MapAccountAggrements(CreateAccountRequest createAccountRequest)
		{
			var agreements = new AlpacaAccountAgreement[3];

			agreements[0] = new AlpacaAccountAgreement()
			{
				agreement = AggrementTypeValue.account_agreement,
				signed_at = DateTimeExtensions.UnixTimeStampToDateTime(createAccountRequest.CustomerAndAccountAgreementSignedAt / 1000).ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"),
				ip_address = createAccountRequest.IpAddress

			};
			agreements[1] = new AlpacaAccountAgreement()
			{
				agreement = AggrementTypeValue.customer_agreement,
				signed_at = DateTimeExtensions.UnixTimeStampToDateTime(createAccountRequest.CustomerAndAccountAgreementSignedAt / 1000).ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"),
				ip_address = createAccountRequest.IpAddress

			};
			agreements[2] = new AlpacaAccountAgreement()
			{
				agreement = AggrementTypeValue.margin_agreement,
				signed_at = DateTimeExtensions.UnixTimeStampToDateTime(createAccountRequest.CustomerAndAccountAgreementSignedAt / 1000).ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"),
				ip_address = createAccountRequest.IpAddress

			};

			return agreements;
		}
	}
}
