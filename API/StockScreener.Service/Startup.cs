using Core.Logging;
using Database.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StockInformation;
using StockScreener.Database;
using StockScreener.Database.Config;
using StockScreener.Database.Repos;
using StockScreener.Database.Repos.Interfaces;
using StockScreener.Interfaces;
using StockScreener.SecurityGrabber;
using System.Text.Json.Serialization;

namespace StockScreener.Service
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
            services.Configure<StockInformationDatabaseSettings>(Configuration.GetSection(nameof(StockInformationDatabaseSettings)));
            services.AddSingleton<IStockInformationDatabaseSettings>(sp => sp.GetRequiredService<IOptions<StockInformationDatabaseSettings>>().Value);

			services.Configure<MyLoggerOptions>(Configuration.GetSection(nameof(MyLoggerOptions)));
			services.AddSingleton<IMyLoggerOptions>(sp => sp.GetRequiredService<IOptions<MyLoggerOptions>>().Value);

			services.AddScoped<IMongoDBContext, MongoStockInformationDbContext>();
            services.AddScoped<IStockFinancialsRepository, StockFinancialsRepository>();
            services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();
            services.AddScoped<IStockIndexRepository, StockIndexRepository>();
            services.AddScoped<IPriceDataRepository>(_ =>new PriceDataRepository(new MongoDbContextFactory()));
            services.AddScoped<ISecuritiesGrabber, SecuritiesGrabber>();
            services.AddScoped<IStockScreenerService, StockScreenerService>();
            services.AddScoped<IStockInformationService, StockInformationService>();
			services.AddSingleton<ILogger, MyLogger>();

			services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockScreener", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockScreener v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
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
