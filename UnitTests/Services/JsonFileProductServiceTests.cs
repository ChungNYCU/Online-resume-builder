using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace UnitTests.Pages.Product.AddRating
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating

        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Product_Id_Should_Return_False()
        {
            // Arrange

            // Act: Look up the product "1000" which doesn't exist, so its data = null
            var result = TestHelper.ProductService.AddRating("not a product id", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

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

        [Test]
        public void AddRating_Rating_Greater_Than_5_Should_False()
        {
            // Arrange

            // Act
            var data = TestHelper.ProductService.GetAllData().First();
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

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


        [Test]
        public void AddRating_Valid_Product_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

    }
}