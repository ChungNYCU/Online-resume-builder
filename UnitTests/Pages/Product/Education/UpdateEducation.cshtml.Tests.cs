using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product.Education;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace UnitTests.Pages.Product.Education
{
    class UpdateEducationTests
    {
        #region TestSetup
        // AddEducation page to test
        public static UpdateEducationExperienceModel pageModel;

        /// <summary>
        /// Initialization for the tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateEducationExperienceModel(TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Valid OnGet should return an valid education record
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_An_Education_Record()
        {
            // Arrange

            // Act
            var Product = TestHelper.ProductService.GetAllData().First();
            var Education = Product.EducationList[0];
            // Act
            pageModel.OnGet(Education.ProductID, Education.ID);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(Education.University, pageModel.Education.University);
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
            pageModel.OnGet(ProductID, "11122");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Education);
        }
        #endregion OnGet


        #region OnPost

        /// <summary>
        /// Invalid OnPost with valid model should return the Index page
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Redirect_to_Index()
        {
            // Arrange
            var Product = TestHelper.ProductService.GetAllData().First();
            pageModel.Education = Product.EducationList[0];

            // Act
            // Expected result for runnning OnPost
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            //Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
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
            pageModel.Education = new EducationModel
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
