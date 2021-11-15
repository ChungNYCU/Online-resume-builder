using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

/// <summary>
/// Unit tests for Product.Read page
/// </summary>
namespace UnitTests.Pages.Product.Read
{
    /// <summary>
    /// Unit tests for Product.Read page
    /// </summary>
    public class ReadTests
    {
        #region TestSetup
        // Page model to be tested
        public static ReadModel pageModel;

        /// <summary>
        /// Initializes tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadModel(TestHelper.ProductService)
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
            pageModel.OnGet("1");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("June Liao", pageModel.Product.FullName);
        }

        /// <summary>
        /// Invalid OnGet should go to to Index Page
        /// </summary>
        [Test]
        public void OnGet_Product_NotValid_Should_Go_To_Index_Page()
        {
            // Arrange 11122 is a non_existing Product ID
            string ProductID = "11122";

            // Act
            pageModel.OnGet(ProductID);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Product);
        }
        #endregion OnGet
    }
}