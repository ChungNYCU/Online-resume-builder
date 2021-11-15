using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Contains pages with the product CRUDi
/// </summary>
namespace ContosoCrafts.WebSite.Pages.Product.WorkExperience
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class AddWorkExperienceModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public AddWorkExperienceModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public WorkExperienceModel WorkExperience { get; set; }


        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id">This is Product ID</param>
        public void OnGet(string id)
        {
            // Get product by looking up product id in JSON file
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));  // id comes from Update.cshtml line 55: <a asp-page="AddEducation" asp-route-id="@Model.Product.Id">+ Add</a>
            if (Product == null)
            {
                RedirectToPage("/Product/Index");
                return;
            }

            // Create a new WorkExperience record and populate it with default value
            WorkExperience = new WorkExperienceModel();

            // Generate a gloable unique id for this WorkExperience record
            WorkExperience.ID = System.Guid.NewGuid().ToString();

            // Put product id onto the WorkExperience record (Match )
            WorkExperience.ProductID = Product.Id;

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

            // What's going to come up is an WorkExperience record who knows the product id on it
            // Get its corresponding Product by matching product id
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(WorkExperience.ProductID));
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            // Add Education record onto WorkExperiences List
            Product.WorkExperienceList.Add(WorkExperience);

            // Update this Product in Json 
            ProductService.UpdateData(Product);

            // Redirect to Product/update page
            return RedirectToPage("/Product/Update", new { Id = Product.Id });
        }
    }
}