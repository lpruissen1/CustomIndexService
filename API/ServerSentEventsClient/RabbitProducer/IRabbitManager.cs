namespace ServerSentEventsClient.RabbitProducer
{
	public interface IRabbitManager
	{
		void Publish<T>(T message)
			where T : class;
	}
}
