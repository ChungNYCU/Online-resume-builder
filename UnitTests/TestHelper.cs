
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Unit tests for the code base
/// </summary>
namespace UnitTests
{
    /// <summary>
    /// Helper to hold the web start settings
    ///
    /// HttpClient
    /// 
    /// Action Contect
    /// 
    /// The View Data and Teamp Data
    /// 
    /// The Product Service
    /// </summary>
    public static class TestHelper
    {
        // Mock web host environment
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;
        // Url helper factory
        public static IUrlHelperFactory UrlHelperFactory;
        // Default http context
        public static DefaultHttpContext HttpContextDefault;
        // Web host environment
        public static IWebHostEnvironment WebHostEnvironment;
        // Model state dictionary
        public static ModelStateDictionary ModelState;
        // Action context
        public static ActionContext ActionContext;
        // Empty model metadata provider
        public static EmptyModelMetadataProvider ModelMetadataProvider;
        // View data dictionary
        public static ViewDataDictionary ViewData;
        // Temporary data dictionary
        public static TempDataDictionary TempData;
        // Page context
        public static PageContext PageContext;
        // Json file product service
        public static JsonFileProductService ProductService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static TestHelper()
        {
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            ModelState = new ModelStateDictionary();

            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            ModelMetadataProvider = new EmptyModelMetadataProvider();
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            ProductService = new JsonFileProductService(MockWebHostEnvironment.Object);

            // Json file product service
            JsonFileProductService productService;

            productService = new JsonFileProductService(TestHelper.MockWebHostEnvironment.Object);
        }
    }
}