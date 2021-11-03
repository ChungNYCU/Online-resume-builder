using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Contains all parts of the website
/// </summary>
namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// The program that runs when the website is called
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Default constructor when the website is run
        /// </summary>
        /// <param name="args"></param>
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
