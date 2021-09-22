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
using Users.Database;
using Users.Database.Config;
using Users.Database.Repositories;
using Users.Database.Repositories.Interfaces;
using Users.Positions;

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

			services.AddControllers();

			services.AddScoped<IMongoDBContext, MongoUserDbContext>();
			services.AddScoped<IPasswordListRepository, PasswordListRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserAccountsRepository, UserAccountsRepository>();
			services.AddScoped<IUserDisclosuresRepository, UserDisclosuresRepository>();
			services.AddScoped<IUserDocumentsRepository, UserDocumentsRepository>();
			services.AddScoped<IUserOrdersRepository, UserOrdersRepository>();
			services.AddScoped<IUserPositionsRepository, UserPositionsRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPositionsService, PositionsService>();
			services.AddScoped<IPositionAdditionHandler, PositionAdditionHandler>();
			services.AddScoped<IAccountsService, AccountsService>();
			services.AddScoped<ITokenGenerator, TokenGenerator>();
			services.AddScoped<IHasher, BCryptHasher>();
			services.AddSingleton<ILogger, MyLogger>();

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
