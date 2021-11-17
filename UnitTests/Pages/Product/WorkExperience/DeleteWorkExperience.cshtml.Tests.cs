using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product.WorkExperience;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace UnitTests.Pages.Product.Education
{
    class DeleteWorkExperienceTests
    {
        #region TestSetup
        // AddEducation page to test
        public static DeleteWorkExperienceModel pageModel;

        /// <summary>
        /// Initialization for the tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteWorkExperienceModel(TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Valid OnGet should return an valid WorkExperience record
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_An_WorkExperience_Record()
        {
            // Arrange

            // Act
            var Product = TestHelper.ProductService.GetAllData().First();
            var WorkExperience = Product.WorkExperienceList[0];
            // Act
            pageModel.OnGet(WorkExperience.ProductID, WorkExperience.ID);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(WorkExperience.Title, pageModel.WorkExperience.Title);
        }

        /// <summary>
        /// Invalid OnGet should go to to Index Page
        /// </summary>
        [Test]
        public void OnGet_InValid_ProductID_Should_Return_Index_Page()
        {
            // Arrange 11122 is a non_existing Product ID
            string ProductID = "11122";

            // Act
            var result = pageModel.OnGet(ProductID, "InValidWorkExperienceID") as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        #endregion OnGet


        #region OnPost

        /// <summary>
        /// Invalid OnPost with valid model should return to Update page
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Redirect_to_Update()
        {
            // Arrange
            var Product = TestHelper.ProductService.GetAllData().First();
            pageModel.WorkExperience = Product.WorkExperienceList[0];

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
            pageModel.WorkExperience = new WorkExperienceModel
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
