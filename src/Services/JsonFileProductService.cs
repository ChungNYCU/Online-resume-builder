using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

/// <summary>
/// Handles the actions to be taken with any model from the controller
/// </summary>
namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Handles the data exchange between the database and the web services
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Default constructor
        /// Builds the Web host environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // Web host environment to be used
        public IWebHostEnvironment WebHostEnvironment { get; }

        // Return string included absolute path to the web-servable, data, and products.json
        private string JsonFileName
        {
            // Return string included absolute path to the web-servable, data, and products.json
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }


        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>IEnumerable<ProductModel></returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            // Read data from JSON file
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }


        /// <summary>
        /// Add rating to product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool AddRating(string productId, int rating)
        {
            if (string.IsNullOrEmpty(productId))
                return false;

            // List of all products from the database
            var products = GetAllData();

            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
                return false;

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
                return false;

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
                return false;

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
                data.Ratings = new int[] { };

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveProducts(products);

            return true;
        }

        /// <summary>
        /// Create product and update to JSON file
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData()
        {
            // Initialize product
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                FullName = "Enter FullName",
                AboutMe = "Enter Description",
                LinkedinUrl = "Enter URL",
                Photo = "Enter Photo URL",
            };

            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            // Update product to JSON file
            SaveProducts(dataSet);

            return data;
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteData(string id, string password)
        {
            // Get the current set, and select product by id
            var dataSet = GetAllData();
            // Get specific data from the dataset by Id
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));
            if (data.Password != password) 
            {
                return false;
            }

            // Make sure the product id is not exist  in JSON file
            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            // Update product to JSON file
            SaveProducts(newDataSet);

            return true;
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateData(ProductModel data)
        {
            // Get the product, and select product by id
            var product = GetAllData();
            // Get specific product from the productList by Id
            var productData = product.FirstOrDefault(x => x.Id.Equals(data.Id));

            // If the productData doesn't exist, return False
            if (productData == null)
            {
                return false;
            }

            // If the Password of the productData doesn't exist,
            // or it doesn't matches data.Password, return False
            if (productData.Password != null) 
            {
                if (productData.Password != data.Password)
                {
                    return false;
                }
            }

            // Update old data with new data
            productData.FullName = data.FullName;
            productData.AboutMe = data.AboutMe;
            productData.AwardList = data.AwardList;
            productData.LinkedinUrl = data.LinkedinUrl;
            productData.Photo = data.Photo;
            productData.EducationList = data.EducationList;
            productData.PersonalSkill = data.PersonalSkill;
            productData.Experiences = data.Experiences;
            productData.Password = data.Password;

            // Save it to JSON file
            SaveProducts(product);

            return true;
        }



        /// <summary>
        /// Write JSON file
        /// </summary>
        /// <param name="products"></param>
        private void SaveProducts(IEnumerable<ProductModel> products)
        {
            // Write JSON file
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Update personal status
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="PersonalStatus"></param>
        /// <returns></returns>
        public bool UpdatePersonalStatus(string productId, string PersonalStatus)
        {
            // Get all product list, and select product by id
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(productId));

            // If productID or PersonalStatus is null, return false
            if (productId == null)
                return false;
            if (PersonalStatus == null)
                return false;

            // Overwrite old data by new data
            productData.PersonalStatus = PersonalStatus;

            // Update product to JSON file
            SaveProducts(products);

            return true;
        }

    }
}