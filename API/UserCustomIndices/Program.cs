
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UserCustomIndices
{
    // https://localhost:44375/swagger/index.html
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
