using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

/// <summary>
/// Unit Tests for Product.Index pages
/// </summary>
namespace UnitTests.Pages.Product.Index
{
    // Unit Tests for Product.Index pages
    public class IndexTests
    {
        #region TestSetup
        // Page context for tests
        public static PageContext pageContext;
        // Page Model to be tested
        public static IndexModel pageModel;

        /// <summary>
        /// Initialization for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new IndexModel(TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Valid OnGet should return products
        /// </summary>
        [Test]
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