using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

/// <summary>
/// Unit tests for Product.Delete page
/// </summary>
namespace UnitTests.Pages.Product.Delete
{
    /// <summary>
    /// Unit tests for Product.Delete page
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        // DeleteModel page to test
        public static DeleteModel pageModel;

        /// <summary>
        /// Initialization for the tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Valid OnGet method should retrun products
        /// </summary>
        #region OnGet
        [Test]
        //[Ignore("Ignore a test")]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("1");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("June Liao", pageModel.Product.FullName);
        }
        #endregion OnGet

        /// <summary>
        /// Valid OnPost should return products
        /// </summary>
        #region OnPost
        [Test]
        //[Ignore("Ignore a test")]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange

            // First Create the product to delete
            pageModel.Product = TestHelper.ProductService.CreateData();
            pageModel.Product.FullName = "Example to Delete";
            TestHelper.ProductService.UpdateData(pageModel.Product);

            // Act
            // Expected result from running OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(false, result.PageName.Contains("Index"));

            
        }

        /// <summary>
        /// Invalid OnPost should return the page
        /// </summary>
        [Test]
        //[Ignore("Ignore a test")]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            // Expected result from running OnPost
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void OnPost_InValid_Should_RedirectTo_Delete_Page()
        {
            // Arrange

            // First Create the product to delete
            pageModel.Product = TestHelper.ProductService.CreateData();
            pageModel.Product.FullName = "Unit Test";
            TestHelper.ProductService.UpdateData(pageModel.Product);

            // Act
            // Expected result from running OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Delete"));
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void OnPost_Valid_Should_RedirectTo_Index_Page()
        {
            // Arrange

            // First Create the product to delete
            pageModel.Product = TestHelper.ProductService.GetAllData().First(x => x.Id == "3");
            pageModel.Product.Password = "0";

            // Act
            // Expected result from running OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        #endregion OnPost
    }
}