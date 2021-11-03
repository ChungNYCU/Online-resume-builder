using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

/// <summary>
/// Establish Unit Test for all functions on the Index Page
/// </summary>
namespace UnitTests.Pages.Index
{
    /// <summary>
    /// Establish Unit Test for all functions on the Index Page
    /// </summary>
    public class IndexTests
    {
        #region TestSetup
        // PageModel to be tested
        public static IndexModel pageModel;

        /// <summary>
        /// Initializes all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Logger
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            pageModel = new IndexModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// This unit test will ensure the OnGet on the Index Page
        /// will return the searching product/resume information
        /// </summary>
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }
        #endregion OnGet
    }
}