using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace ServerSentEventsClient.RabbitProducer
{
	public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
	{
		private readonly IConnection connection;

		public RabbitModelPooledObjectPolicy()
		{
			connection = GetConnection();
		}

		private IConnection GetConnection()
		{
			var factory = new ConnectionFactory()
			{
				HostName = "localhost"
			};

			return factory.CreateConnection();
		}

		public IModel Create()
		{
			return connection.CreateModel();
		}

		public bool Return(IModel obj)
		{
			if (obj.IsOpen)
			{
				return true;
			}
			else
			{
				obj?.Dispose();
				return false;
			}
		}
	}
}
