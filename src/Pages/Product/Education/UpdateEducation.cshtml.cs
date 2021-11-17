using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Contains pages with the product CRUDi
/// </summary>
namespace ContosoCrafts.WebSite.Pages.Product.Education
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class UpdateEducationExperienceModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="productService"></param>
        public UpdateEducationExperienceModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public EducationModel Education { get; set; }


        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="EducationId"></param>
        public IActionResult OnGet(string ProductId, string EducationId)
        {
            // Get product by looking up product id in JSON file
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(ProductId));  // id comes from Update.cshtml line 55: <a asp-page="AddEducation" asp-route-id="@Model.Product.Id">+ Add</a>
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
                
            }

            // Find coresponding education record, assign it to Property Education
            var EducationList = Product.EducationList;
            foreach (EducationModel EducationRecord in EducationList)
            {
                if (EducationRecord.ID == EducationId)
                    Education = EducationRecord;
            }

            return Page();
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
            // Get its corresponding Product by matching product idmn
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(Education.ProductID));
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            // Modify Product's corresponding Education record
            for (int index = 0; index < Product.EducationList.Count; index ++)
            {
                if(Education.ID == Product.EducationList[index].ID)
                {
                    Product.EducationList[index] = Education;
                }
            }

            // Update this Product in Json 
            ProductService.UpdateData(Product);

            // Redirect to Product/update page
            return RedirectToPage("/Product/Update", new { Id = Product.Id });
        }
    }
}