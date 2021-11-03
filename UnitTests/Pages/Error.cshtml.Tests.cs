using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

/// <summary>
/// Establish a Unit Test for all functions on Error Page
/// </summary>
namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Establish a Unit Test for all functions on Error Page
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        // Page to be tested
        public static ErrorModel pageModel;

        /// <summary>
        /// Initialization for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Logger
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Valid Activity for OnGet should return RequestId 
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            // Activity for tests
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// This unit test ensure the OnGet function on the Error Page
        /// will return trace identifier and the request id will be displayed
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }
        #endregion OnGet
    }
}