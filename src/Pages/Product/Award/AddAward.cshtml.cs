using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Contains pages with the product CRUDi
/// </summary>
namespace ContosoCrafts.WebSite.Pages.Product.Award
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class AddAwardModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public AddAwardModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public AwardModel Award { get; set; }


        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id">This is Product ID</param>
        public void OnGet(string id)
        {
            // Get product by looking up product id in JSON file
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));  // id comes from Update.cshtml line 55: <a asp-page="AddEducation" asp-route-id="@Model.Product.Id">+ Add</a>
            
            // Create a new Education record and populate it with default value
            Award = new AwardModel();

            // Generate a gloable unique id for this Education record
            Award.ID = System.Guid.NewGuid().ToString();

            // Put product id onto the Education record (Match )
            Award.ProductID = Product.Id;

        }



        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Product
        /// Call the data layer to Update that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {

            // If ModelState is not valid, return Page()
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // What's going to come up is an education record who knows the product id on it
            // Get its corresponding Product by matching product id
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(Award.ProductID));

            // Add Education record onto EducationList
            Product.AwardList.Add(Award);

            // Update this Product in Json 
            ProductService.UpdateData(Product);

            // Redirect to Product/Index page
            return RedirectToPage("/Product/Index");
        }
    }
}