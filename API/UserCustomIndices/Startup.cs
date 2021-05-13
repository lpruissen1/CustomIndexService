using AutoMapper;
using Database;
using Database.Core;
using Database.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using UserCustomIndices.Database.Config;
using UserCustomIndices.Mappers;
using UserCustomIndices.Services;
using UserCustomIndices.Validators;

namespace UserCustomIndices
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
            services.Configure<CustomIndexDatabaseSettings>(Configuration.GetSection(nameof(CustomIndexDatabaseSettings)));
            services.AddSingleton<ICustomIndexDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CustomIndexDatabaseSettings>>().Value);

            services.AddControllers();
            services.AddScoped<IMongoDBContext, MongoCustomIndexDbContext>();
            services.AddScoped<IIndicesRepository, IndiciesRepository>();
            services.AddScoped<ICustomIndexService, CustomIndexService>();
            services.AddScoped<ICustomIndexValidator, CustomIndexValidator>();
            services.AddScoped<IRequestMapper, RequestMapper>();

            services.AddSingleton(_ => MapperConfigurationRepository.Create());
            services.AddScoped<IMapper, Mapper>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomIndex", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomIndex v1"));
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
