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
    /// Delete of the data for a single record 
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]

        /// getting the product(resume) 
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
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Product
        /// Call the data layer to Delete that data
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

            // encrypt password
            Product.Password = Crypto.AESEncryption.Encrypt(Product.Password, Product.Id);

            // If password match, delete data. If not, go back to delete page
            if (ProductService.DeleteData(Product.Id, Product.Password) == false)
            {
                return RedirectToPage("./Delete");
            }

            //Redirect to Product/Index page
            return RedirectToPage("./Index");
        }
    }
}