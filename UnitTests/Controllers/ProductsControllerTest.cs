using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Tests for Controllers folder
/// </summary>
namespace UnitTests.Controllers
{
    /// <summary>
    /// Tests for Controller coverage
    /// </summary>
    class ProductsControllerTest
    {
        // Holds controller for testing
        ProductsController productsController;

        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public void TestInitialize()
        {
            // Uses the TestHelper Product Service as the Service to be passed to the controller
            productsController = new ProductsController(TestHelper.ProductService);
        }

        /// <summary>
        /// Tests the Constructor in ProductsController
        /// </summary>
        [Test]
        public void Constructor_Valid_Return_TestHelper_ProductService()
        {
            // Arrange
            TestInitialize();

            // Act
            // Expected results for the test
            var result = productsController.ProductService;

            // Assert
            Assert.AreEqual(TestHelper.ProductService, result);
        }

        /// <summary>
        /// Tests the Get method in ProductsController
        /// </summary>
        [Test]
        public void Get_Valid_Return_TestHelper_ProductService()
        {
            // Arrange
            TestInitialize();

            // Act
            // Expected results for the test
            var result = productsController.Get();
            // Count to iterate through the product list being retrieved for Asserts
            var count = 0;
            List<ProductModel> getAllData = new List<ProductModel>(TestHelper.ProductService.GetAllData());

            // Assert
            foreach (var product in getAllData)
            {
                Assert.AreEqual(product.Id, result.ElementAt(count).Id);
                count++;
            }
        }

        /// <summary>
        /// Tests the Patch method in ProductsController
        /// </summary>
        [Test]
        public void Patch_Valid_Return_Ok_Should_Not_Be_Null()
        {
            // Arrange
            TestInitialize();

            // Sample request to run the Patch method 
            ProductsController.RatingRequest request = new ProductsController.RatingRequest();
            request.ProductId = "yes";
            request.Rating = 1;

            // Act
            var result = productsController.Patch(request);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}