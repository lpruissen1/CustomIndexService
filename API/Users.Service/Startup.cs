using Core.Logging;
using Database.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Users.Core;
using Users.CustomIndices;
using Users.Database;
using Users.Database.Config;
using Users.Database.Repositories;
using Users.Database.Repositories.Interfaces;
using Users.Funding;
using Users.Orders;
using Users.Positions;
using Users.RabbitListener;

namespace Users.Service
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

			services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings)));
			services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

			services.Configure<JwtConfiguration>(Configuration.GetSection(nameof(JwtConfiguration)));
			services.AddSingleton<IJwtConfiguration>(sp => sp.GetRequiredService<IOptions<JwtConfiguration>>().Value);

			services.Configure<MyLoggerOptions>(Configuration.GetSection(nameof(MyLoggerOptions)));
			services.AddSingleton<IMyLoggerOptions>(sp => sp.GetRequiredService<IOptions<MyLoggerOptions>>().Value);
			services.AddSingleton<ILogger, MyLogger>();

			services.AddControllers();

			services.AddScoped<IIndicesRepository, IndiciesRepository>();
			services.AddScoped<ICustomIndexService, CustomIndexService>();
			services.AddScoped<IRequestMapper, RequestMapper>();
			services.AddScoped<IResponseMapper, ResponseMapper>();

			services.AddSingleton<IMongoDBContext, MongoUserDbContext>();
			services.AddSingleton<IPasswordListRepository, PasswordListRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IUserAccountsRepository, UserAccountsRepository>();
			services.AddSingleton<IUserDisclosuresRepository, UserDisclosuresRepository>();
			services.AddSingleton<IUserDocumentsRepository, UserDocumentsRepository>();
			services.AddSingleton<IUserOrdersRepository, UserOrdersRepository>();
			services.AddSingleton<IUserTransfersRepository, UserTransfersRepository>();
			services.AddSingleton<IUserPositionsRepository, UserPositionsRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IFundingService, FundingService>();
			services.AddScoped<IOrderService, OrderSerivce>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPositionsService, PositionsService>();
			services.AddScoped<IAccountsService, AccountsService>();

			services.AddSingleton<IPositionUpdateHandler, PositionUpdateHandler>();
			services.AddSingleton<ITransferUpdateHandler, TransferUpdateHandler>();

			services.AddScoped<ITokenGenerator, TokenGenerator>();
			services.AddScoped<IHasher, BCryptHasher>();

			services.AddHostedService<PositionsListener>();

			services.AddControllers().AddJsonOptions(o =>
			{
				o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.Service v1"));
            }

			app.UseCookiePolicy(new CookiePolicyOptions 
			{ 
				HttpOnly = HttpOnlyPolicy.Always 
			});

            app.UseHttpsRedirection();

            app.UseRouting();

			app.UseCors(options =>
			{
				options.WithOrigins("https://localhost:6001")
					.AllowAnyHeader()
					.AllowAnyMethod();
			});

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
