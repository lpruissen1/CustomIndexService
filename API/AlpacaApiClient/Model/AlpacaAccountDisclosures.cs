namespace AlpacaApiClient.Model
{
	public class AlpacaAccountDisclosures
	{
		public bool is_control_person { get; set; }
		public bool is_affiliated_exchange_or_finra { get; set; }
		public bool is_politically_exposed { get; set; }
		public bool immediate_family_exposed { get; set; }
	}
}
