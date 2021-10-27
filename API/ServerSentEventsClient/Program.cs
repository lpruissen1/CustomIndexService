using Core.Logging;
using Database.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users;
using Users.Database;
using Users.Database.Config;
using Users.Database.Repositories;
using Users.Database.Repositories.Interfaces;

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

					services.Configure<UserDatabaseSettings>(configuration.GetSection(nameof(UserDatabaseSettings)));
					services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

					services.Configure<MyLoggerOptions>(configuration.GetSection(nameof(MyLoggerOptions)));
					services.AddSingleton<IMyLoggerOptions>(sp => sp.GetRequiredService<IOptions<MyLoggerOptions>>().Value);

					services.AddSingleton<IMongoDBContext, MongoUserDbContext>();

					services.AddSingleton<IUserPositionsRepository, UserPositionsRepository>();
					services.AddSingleton<IUserAccountsRepository, UserAccountsRepository>();
					services.AddSingleton<IUserOrdersRepository, UserOrdersRepository>();
					services.AddSingleton<IPositionAdditionHandler, PositionUpdateHandler>();
					services.AddSingleton<ILogger, MyLogger>();

					services.AddHostedService<Worker>();
                });
    }
}
