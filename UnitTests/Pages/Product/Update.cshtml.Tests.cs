
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;
using System.Linq;

/// <summary>
/// Unit Tests for Product.Update page
/// </summary>
namespace UnitTests.Pages.Product.Update
{
    /// <summary>
    /// Unit Tests for Product.Update page
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        // Page model to be tested
        public static UpdateModel pageModel;

        /// <summary>
        /// Initializes for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.ProductService)
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
        #endregion OnGet

        #region OnPost

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Redirect_to_Index()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "1",
                Password = "0"
            };


            // Act
            // Expected result for runnning OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            //Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        
        /// <summary>
        /// Valid OnPost should return products
        /// </summary>
        [Test]
        public void OnPost_InValid_password_Should_ReDirect_to_Update_page()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "selinazawacki-moon",
                FullName = "FullName",
                AboutMe = "description",
                LinkedinUrl = "url",
                Photo = "image"
            };

    
            // Act
            // Expected result for runnning OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Update"));
        }

        /// <summary>
        /// Invalid OnPost with invalid model should return the page
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
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


        
        #endregion OnPost
    }
}