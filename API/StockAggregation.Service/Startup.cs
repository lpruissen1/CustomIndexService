using ApiClient;
using Core.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StockAggregation.Core;
using StockScreener.Database;
using StockScreener.Database.Config;

namespace StockAggregation.Service
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

			services.AddScoped<IStockAggregationService, StockAggregationService>();
			services.AddScoped<IApiSettingsFactory, ApiSettingsFactory>();
			services.AddScoped<IMongoDbContextFactory, MongoDbContextFactory>();
			services.AddSingleton<ILogger, MyLogger>();

			services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockAggregation.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockAggregation.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
