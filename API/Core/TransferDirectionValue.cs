namespace Core
{
	public enum TransferDirectionValue
	{
		INCOMING,
		OUTGOING
	}

	public enum OrderType
	{
		market,
		limit,
		stop_limit,
		trialing_stop
	}

	public enum OrderExecutionTimeframeValue
	{
		day,
		gtc,
		opg,
		cls,
		ioc,
		fok
	}
}
