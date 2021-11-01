using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// home page of the web application
/// showing all the resume to the viewer
/// allow user to see or modify their resume
/// </summary>
namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Gets the <see cref="T:Microsoft.AspNetCore.Mvc.RazorPages.PageContext" />.
    /// </summary>
    public class IndexModel : PageModel
    {
        // logger
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        // Data to show
        public JsonFileProductService ProductService { get; }
        // getting resume data from json
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// REST OnGet, return all data
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }
    }
}