using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Bulid host and run
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Bulid host and run
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
