using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

/// <summary>
/// Unit Tests for all functions on Privacy Page
/// </summary>
namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Unit Tests for all functions on Privacy Page
    /// </summary>
    public class PrivacyTests
    {
        #region TestSetup
        // PageModel to be tested
        public static PrivacyModel pageModel;

        /// <summary>
        /// To initialize the unit test for privacy page
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Logger
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Curently no new function is created in the Privacy Page so no
        /// new unit testing is created yet.
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