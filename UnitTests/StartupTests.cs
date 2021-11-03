using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;

/// <summary>
/// Establish unit tests for all functions on the Startup.cs 
/// </summary>
namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// Establish unit tests for all functions on the Startup.cs 
    /// </summary>
    public class StartupTests
    {
        #region TestSetup
        /// <summary>
        /// Initializes for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Startup class for tests
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            /// <summary>
            /// Configurations for startup
            /// </summary>
            /// <param name="config"></param>
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        #region ConfigureServices
        /// <summary>
        /// This unit test to check the ConfigureService funtion would get pass
        /// if the webhost name is valid
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            // Webhost build for tests
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }
        #endregion ConfigureServices

        #region Configure
        /// <summary>
        /// This unit test to check the Configure function would get pass
        /// if the webhost name is valid
        /// </summary>
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            // Webost build for tests
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }
        #endregion Configure
    }
}