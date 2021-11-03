using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ServerSentEventsClient.RabbitProducer
{
	public class RabbitManager : IRabbitManager
	{
		private readonly DefaultObjectPool<IModel> _objectPool;

		public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy, ILogger logger)
		{
			_objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
			this.logger = logger;
		}

		private ILogger logger { get; }

		public void Publish<T>(T message)
			where T : class
		{
			if (message == null)
				return;

			var channel = _objectPool.Get();

			try
			{
				var stringMessage = JsonConvert.SerializeObject(message);
				logger.LogInformation(new EventId(1), stringMessage);

				var sendBytes = Encoding.UTF8.GetBytes(stringMessage);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish("", "positions", properties, sendBytes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				_objectPool.Return(channel);
			}
		}
	}
}
