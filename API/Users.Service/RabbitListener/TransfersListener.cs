using AlpacaApiClient.Model.Response.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Users.RabbitListener
{
	internal class TransfersListener : BackgroundService
	{
		private readonly ILogger logger;
		private IConnection _connection;
		private IModel _channel;

		private ITransferUpdateHandler transferUpdateHandler { get; }

		public TransfersListener(ILogger logger, ITransferUpdateHandler transferUpdateHandler)
		{
			this.logger = logger;
			this.transferUpdateHandler = transferUpdateHandler;
			InitRabbitMQ();
		}

		private void InitRabbitMQ()
		{
			var factory = new ConnectionFactory { HostName = "localhost" };

			// create connection  
			_connection = factory.CreateConnection();

			// create channel  
			_channel = _connection.CreateModel();

			_channel.QueueDeclare("transfers", false, false, false, null);

			_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ch, ea) =>
			{
				// received message json 
				var content = Encoding.UTF8.GetString(ea.Body.ToArray());

				// handle the received message  
				HandleMessage(content);
			};

			consumer.Shutdown += OnConsumerShutdown;
			consumer.Registered += OnConsumerRegistered;
			consumer.Unregistered += OnConsumerUnregistered;
			consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

			_channel.BasicConsume("positions", true, consumer);
			return Task.CompletedTask;
		}

		private void HandleMessage(string content)
		{
			logger.LogInformation($"consumer received {content}");

			var deserialized = JsonConvert.DeserializeObject<TransferEvent>(content);

			transferUpdateHandler.UpdateTransfer(deserialized);
		}

		private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
		private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
		private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
		private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
		private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

		public override void Dispose()
		{
			_channel.Close();
			_connection.Close();
			base.Dispose();
		}
	}
}
