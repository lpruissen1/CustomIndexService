using Users.Core;

namespace AlpacaApiClient.Model.Request
{
	public class AlpacaAccountIdentity
	{
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string date_of_birth { get; set; }
		public string tax_id { get; set; }
		public TaxIdTypeValue tax_id_type { get; set; }
		public string country_of_citizenship { get; set; }
		public string country_of_birth { get; set; }
		public string country_of_tax_residence { get; set; }
		public FundingSourceValue[] funding_source { get; set; }
	}
}
