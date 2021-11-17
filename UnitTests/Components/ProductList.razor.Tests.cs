
using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using ContosoCrafts.WebSite.Services;
using System.Linq;

/// <summary>
/// Unit tests for components
/// </summary>
namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for ProductList razor page
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Initializer for unit tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        /// <summary>
        /// Tests for simple ProductList loading
        /// </summary>
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("June Liao"));
        }

        #region SelectProduct
        /// <summary>
        /// Test for valid SelectProduc selection
        /// </summary>
        [Test]
        public void SelectProduct_Valid_ID_1_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_1";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }
        #endregion SelectProduct

        #region SubmitRating

        /// <summary>
        /// Test for valid Submit rating with no prior rating
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_1";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, postVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        /// <summary>
        /// Test for submit rating with prior rating
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_2";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the Last star item from the list, it should one that is checked
            var starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("6 Votes"));
            Assert.AreEqual(true, postVoteCountString.Contains("7 Votes"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating

        #region Search
        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_InValid_Empty_Should_Return_All_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_InValid_Space_Should_Return_All_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("     ");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Name_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Toby Mccullough");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Toby Mccullough"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_LinkedIn_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Bart");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Bart Joseph"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Personal_Status_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Fine I guess");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Bart Joseph"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Photo_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("smartrecruiters");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Award_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Scholarship");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Issuer_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Phi Theta Kappa");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Bart"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Work_Title_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("PM");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Work_Employer_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("OPPO");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Work_Description_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Manager");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Personal_Skill_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Java");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_About_Me_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Seeking for 2022 intern");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Education_University_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Seattle");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Education_Location_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("WA");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Education_Degree_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("MS");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }

        /// <summary>
        /// Test for valid Search
        /// </summary>
        [Test]
        public void Search_Valid_Education_Major_Should_Return_Matching_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("CS");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }
        #endregion Search

        #region Clear
        /// <summary>
        /// Test for valid Clear
        /// </summary>
        [Test]
        public void Clear_Valid_Search_Should_Return_All_Content()
        {
            // Fill productList with Bart
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Search_ID";
            var id2 = "SearchCriteria_ID";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var inputList = page.FindAll("input");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            var search = inputList.First(m => m.OuterHtml.Contains(id2));

            // Act
            search.Change("Bart");
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Bart Joseph"));

            // Clear and search for non-Bart
            // Arrange
            var id3 = "Clear_ID";

            // Find the Buttons (more info)
            buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            button = buttonList.First(m => m.OuterHtml.Contains(id3));

            // Act
            button.Click();

            // Get the markup to use for the assert
            pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("June Liao"));
        }
        #endregion Clear
    }
}