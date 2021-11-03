using Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServerSentEventsClient.RabbitProducer;

namespace ServerSentEventsClient
{
	public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
				{
					IConfiguration configuration = hostContext.Configuration;

					services.Configure<MyLoggerOptions>(configuration.GetSection(nameof(MyLoggerOptions)));
					services.AddSingleton<IMyLoggerOptions>(sp => sp.GetRequiredService<IOptions<MyLoggerOptions>>().Value);
					services.AddSingleton<ILogger, MyLogger>();

					// rabbit 
					services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
					services.AddSingleton<IRabbitManager, RabbitManager>();

					services.AddHostedService<Worker>();
                });
    }
}
