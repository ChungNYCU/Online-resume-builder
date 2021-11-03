using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

/// <summary>
/// This conrolls the interaction between the pages and the services
/// </summary>
namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /// <summary>
    /// A base class for an MVC controller without view support.
    /// </summary>
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// getting data from jasonFileProductService.cs
        public JsonFileProductService ProductService { get; }


        [HttpGet]
        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        [HttpPatch]
        /// implement a rating system
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);

            return Ok();
        }
        /// <summary>
        /// Asking for rating of the product
        /// </summary>
        public class RatingRequest
        {
            /// Id of the product
            public string ProductId { get; set; }
            /// Rating of the product
            public int Rating { get; set; }
        }
    }
}