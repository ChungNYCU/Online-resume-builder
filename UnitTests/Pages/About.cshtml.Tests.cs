using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

/// <summary>
/// Establish a Unit Test for all functions on About Page
/// </summary>
namespace UnitTests.Pages.About
{
    /// <summary>
    /// Establish a Unit Test for all functions on About Page
    /// </summary>
    public class AboutTests
    {
        #region TestSetup
        // About page model to be tested
        public static AboutModel pageModel;

        /// <summary>
        /// Initializes for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Logger
            var MockLoggerDirect = Mock.Of<ILogger<AboutModel>>();

            pageModel = new AboutModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Currently no function is craeted on About page so no unit testing function yet.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnGet
    }
}