using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class UpdateModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        [BindProperty]
        public WorkExperienceModel[] WorkExperience { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            //Get product by id in JSON file
            Product  = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            WorkExperience = ProductService.GetAllWorkData().Where(c => c.CandidateId.Equals(id)).ToArray();
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
            //If ModelState is not valid, return Page()
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //If ModelState is valid, update data to JSON file
            ProductService.UpdateData(Product);
            ProductService.UpdateWorkData(WorkExperience);

            //Redirect to Product/Index page
            return RedirectToPage("./Index");
        }
    }
}