namespace Core
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
		pending_replace,
		pending_new,
		accepted_for_bidding,
		stopped,
		rejected,
		suspended,
		calculated
	}

	public enum OrderDirectionValue
	{
		buy,
		sell
	}
}
