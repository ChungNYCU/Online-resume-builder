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
    public class UpdateAwardModel : PageModel
    {
        /// Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="productService"></param>
        public UpdateAwardModel(JsonFileProductService productService)
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
        /// <param name="ProductId"></param>
        /// <param name="AwardId"></param>
        public void OnGet(string ProductId, string AwardId)
        {
            // Get product by looking up product id in JSON file
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(ProductId));  // id comes from Update.cshtml line 55: <a asp-page="AddEducation" asp-route-id="@Model.Product.Id">+ Add</a>

            // Find coresponding education record, assign it to Property Education
            var AwardList = Product.AwardList;
            foreach (AwardModel AwardRecord in AwardList)
            {
                if (AwardRecord.ID == AwardId)
                    Award = AwardRecord;
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

            // What's going to come up is an education record who knows the product id on it
            // Get its corresponding Product by matching product idmn
            var Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(Award.ProductID));

            // Modify Product's corresponding Education record
            for (int index = 0; index < Product.AwardList.Count; index ++)
            {
                if(Award.ID == Product.AwardList[index].ID)
                {
                    Product.AwardList[index] = Award;
                }
            }

            // Update this Product in Json 
            ProductService.UpdateData(Product);
   
            // Redirect to Product/Index page
            return RedirectToPage("/Product/Index");
        }
    }
}