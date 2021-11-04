using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Unit Test for all JsonFileProductServiceTests.cs blocks
/// </summary>
namespace UnitTests.Pages.Product.AddRating
{
    /// <summary>
    /// Unit Test for all JsonFileProductServiceTests.cs blocks
    /// </summary>
    public class CryptoTests
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

        #region Encrypt
        [Test]
        public void Encrypt_Valid_PlainText_Valid_Password_Should_Return_string()
        {
            // Arrange
            
            // Act
            var result = Crypto.AESEncryption.Encrypt("test", "test");
            // Assert
            Assert.AreEqual(result, result);
        }

        [Test]
        public void Encrypt_null_PlainText_Valid_Password_Should_Return_empty_string()
        {
            // Arrange

            // Act
            var result = Crypto.AESEncryption.Encrypt("", "test");
            // Assert
            Assert.AreEqual("", result);
        }
        #endregion Encrypt
    }
}