using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// Contains pages with the product CRUDi
/// </summary>
namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>
    public class UpdateModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }


        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            //Get product by id in JSON file
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                RedirectToPage("./Index");
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

            // encrypt password
            Product.Password = Crypto.AESEncryption.Encrypt(Product.Password, Product.Id);

            // Get EducationList, AwardList
            var TempProduct = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(Product.Id));
            if (TempProduct != null)
            {
                Product.EducationList = TempProduct.EducationList;
                Product.AwardList = TempProduct.AwardList;
                Product.WorkExperienceList = TempProduct.WorkExperienceList;
            }

            // If password match, update data. If not, go back to update page
            if (ProductService.UpdateData(Product) == false) 
            {
                return RedirectToPage("./Update");
            }

            //Redirect to Product/Index page
            return RedirectToPage("./Index");
        }
    }
}