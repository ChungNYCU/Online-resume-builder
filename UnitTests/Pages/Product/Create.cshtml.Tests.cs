using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

/// <summary>
/// Unit Tests for the Product.Create page
/// </summary>
namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Unit Tests for the Product.Create page
    /// </summary>
    public class CreateTests
    {
        // Create page model for testing
        #region TestSetup
        public static CreateModel pageModel;

        /// <summary>
        /// Initialization for the tests to be made
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Valid OnGet test that should return products
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            var oldCount = TestHelper.ProductService.GetAllData().Count();

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(oldCount+1, TestHelper.ProductService.GetAllData().Count());
        }
        #endregion OnGet
    }
}