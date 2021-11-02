using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using NUnit.Framework;

/// Establish unit tests for all functions on the Startup.cs 
namespace UnitTests.Pages.Startup
{
    public class StartupTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        #region ConfigureServices
        // This unit test to check the ConfigureService funtion would get pass
        // if the webhost name is valid
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }
        #endregion ConfigureServices

        // This unit test to check the Configure function would get pass
        // if the webhost name is valid
        #region Configure
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion Configure
    }
}