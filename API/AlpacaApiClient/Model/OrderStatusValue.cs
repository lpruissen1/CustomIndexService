namespace AlpacaApiClient.Model
{
	public enum OrderStatusValue
	{
		New,
		accepted,
		partially_filled,
		filled,
		done_for_day,
		canceled,
		expired,
		replaced,
		pending_cancel,
		pending_replace
	}
}
