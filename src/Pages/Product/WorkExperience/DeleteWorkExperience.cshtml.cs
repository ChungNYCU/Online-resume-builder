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
    public class DeleteWorkExperienceModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="productService"></param>
        public DeleteWorkExperienceModel(JsonFileProductService productService)
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
        /// <param name="ProductId"></param>
        /// <param name="WorkExperienceId"></param>
        public void OnGet(string ProductId, string WorkExperienceId)
        {
            // Get product by looking up product id in JSON file
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(ProductId));  
            if (Product == null)
            {
                RedirectToPage("/Product/Index");
                return;
            }

            // Find coresponding work experience record, assign it to this.WorkExperience
            var WorkExperienceList = Product.WorkExperienceList;
            foreach (WorkExperienceModel WorkExpeirenceRecord in WorkExperienceList)
            {
                if (WorkExpeirenceRecord.ID == WorkExperienceId)
                    WorkExperience = WorkExpeirenceRecord;
            }
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

            // What's going to come up is an work experience record who knows the product id on it
            // Get its corresponding Product by matching product idmn
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(WorkExperience.ProductID));
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            // Find Education record, remove it from Product.EducationList
            for (int index = 0; index < Product.WorkExperienceList.Count; index++)
            {
                if (WorkExperience.ID == Product.WorkExperienceList[index].ID)
                {
                    Product.WorkExperienceList.RemoveAt(index);
                }
            }

            // Update this Product in Json 
            ProductService.UpdateData(Product);

            // Redirect to Product/update page
            return RedirectToPage("/Product/Update", new { Id = Product.Id });
        }
    }
}