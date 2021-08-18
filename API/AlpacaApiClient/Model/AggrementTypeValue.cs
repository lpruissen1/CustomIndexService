namespace AlpacaApiClient.Model
{
	public enum AggrementTypeValue
	{
		margin_agreement,
		account_agreement,
		customer_agreement
	}
	public enum AlpacaAccountStatusValue
	{
		SUBMITTED,
		ACTION_REQUIRED,
		APPROVAL_PENDING,
		APPROVED,
		REJECTED,
		ACTIVE,
		DISABLED,
		ACCOUNT_CLOSED
	}
	public enum AlpacaKycResults
	{
		IDENTITY_VERIFICATION,
		TAX_IDENTIFICATION,
		ADDRESS_VERIFICATION,
		DATE_OF_BIRTH,
		PEP,
		FAMILY_MEMBER_PEP,
		CONTROL_PERSON,
		AFFILIATED,
		OTHER
	}
}
