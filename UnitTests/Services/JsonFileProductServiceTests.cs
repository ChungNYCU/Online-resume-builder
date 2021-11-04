using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;
using System.Linq;

/// <summary>
/// Unit Test for all JsonFileProductServiceTests.cs blocks
/// </summary>
namespace UnitTests.Pages.Product.AddRating
{
    /// <summary>
    /// Unit Test for all JsonFileProductServiceTests.cs blocks
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup
        /// <summary>
        /// Initializations for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        #region AddRating
        /// <summary>
        /// Invalid null product in Addrating should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            // Expected result
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid product id in Addrating should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Id_Should_Return_False()
        {
            // Arrange

            // Act
            // Look up the product "1000" which doesn't exist, so its data = null
            var result = TestHelper.ProductService.AddRating("not a product id", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// AddRating with less than 0 should return false
        /// </summary>
        [Test]
        public void AddRating_Rating_Less_Than_0_Should_Return_False()
        {
            // Arrange

            // Act
            var data = TestHelper.ProductService.GetAllData().First();
            var result = TestHelper.ProductService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Rating greater than 5 should return false
        /// </summary>
        [Test]
        public void AddRating_Rating_Greater_Than_5_Should_False()
        {
            // Arrange

            // Act
            // First item in the product list
            var data = TestHelper.ProductService.GetAllData().First();
            // Expected result when running AddRating
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid ratings with Null in AddRating should set Ratings to not null
        /// </summary>
        [Test]
        public void AddRating_InValid_Ratings_Null_Should_Set_Ratings_Not_Null()
        {
            // Arrange

            // Act
            // Get a data whose rating is null
            var nullRatingData = TestHelper.ProductService.GetAllData().First(x => x.Ratings == null);
            // This data's rating should become not null after executing the following statement
            var result = TestHelper.ProductService.AddRating(nullRatingData.Id, 4);
            // Re-read the data
            var data = TestHelper.ProductService.GetAllData().First(x => x.Id == nullRatingData.Id);

            // Assert
            Assert.AreEqual(false, data.Ratings == null);
        }

        /// <summary>
        /// Valid product rating in AddRating should return true
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();
            // Origial count
            var countOriginal = data.Ratings.Length;

            // Act
            // Expected result
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            // Gets the first product from the product list
            var dataNewList = TestHelper.ProductService.GetAllData().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

        #region UpdatePersonalStatus
        [Test]
        public void UpdatePersonalStatus_InValid_productId_Null_Should_Return_False()
        {
            // Act
            var result = TestHelper.ProductService.UpdatePersonalStatus(null, "test");
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void UpdatePersonalStatus_InValid_PersonalStatus_Null_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData().First().Id;
            // Act
            var result = TestHelper.ProductService.UpdatePersonalStatus(data, null);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void UpdatePersonalStatus_Valid_productId_Valid_PersonalStatus_Should_Return_True()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData().First().Id;
            // Act
            var result = TestHelper.ProductService.UpdatePersonalStatus(data, "test");
            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion UpdatePersonalStatus
    }
}