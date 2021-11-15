using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product.Award;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace UnitTests.Pages.Product.Award
{
    class AddAwardTests
    {
        #region TestSetup
        // AddAward page to test
        public static AddAwardModel pageModel;

        /// <summary>
        /// Initialization for the tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new AddAwardModel(TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Valid OnGet should return an valid Award record
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_An_Award_Record()
        {
            // Arrange

            // Act
            var Product = TestHelper.ProductService.GetAllData().First();

            // Act
            pageModel.OnGet(Product.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(false, pageModel.Award.ID == null);
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
            Assert.AreEqual(null, pageModel.Award);
        }
        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Invalid OnPost with valid model should return the Update page
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Redirect_to_Update()
        {
            // Arrange
            var Product = TestHelper.ProductService.GetAllData().First();
            pageModel.Award = new AwardModel
            {
                ProductID = Product.Id,
                ID = System.Guid.NewGuid().ToString()
        };


            // Act
            // Expected result for runnning OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Update"));
        }

        /// <summary>
        /// Invalid OnPost with invalid model should return the page
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Should_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            // Expected result from running the OnPost
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Invalid OnPost with invalid model should return the Index page
        /// </summary>
        [Test]
        public void OnPost_InValid_Award_Should_Return_index_Page()
        {
            // Arrange
            pageModel.Award = new AwardModel
            {
                ProductID = "11122", // 11122 is a non_existing Product ID
                ID = System.Guid.NewGuid().ToString()
            };

            // Act
            // Expected result from running the OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        #endregion OnPost
    }
}
