namespace Users.Core.Request
{
	public class CreateAchRelationshipRequest
	{
		public string AlpacaAccountId { get; set; }
		public string BankAccountOwnerName { get; set; }
		public  AccountTypeValue AccountType { get; set; }
		public string BankAccountNumber { get; set; }
		public string BankAccountRoutingNumber { get; set; }
		public string BankAccountNickname { get; set; }
	}
}
