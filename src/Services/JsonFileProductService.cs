using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            // Return string included absolute path to the web-servable, data, and products.json
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        private string WorkExperienceFileName
        {
            // Return string included absolute path to the web-servable, data, and workExperience.json
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "workExperience.json"); }
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
        /// Get all work experience data
        /// </summary>
        /// <returns>IEnumerable<WorkExperienceModel></returns>
        public IEnumerable<WorkExperienceModel> GetAllWorkData()
        {
            using (var jsonFileReader = File.OpenText(WorkExperienceFileName))
            {
                return JsonSerializer.Deserialize<WorkExperienceModel[]>(jsonFileReader.ReadToEnd(),
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
                Photo = "",
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
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and select product by id
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            // Make sure the product id is not exist  in JSON file
            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            // Update product to JSON file
            SaveProducts(newDataSet);

            return data;
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProductModel UpdateData(ProductModel data)
        {
            // Get all product list, and select product by id
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }

            // Overwrite old data by new data
            productData.FullName = data.FullName;
            productData.AboutMe = data.AboutMe;
            productData.Awards = data.Awards;
            productData.LinkedinUrl = data.LinkedinUrl;
            productData.Photo = data.Photo;
            productData.EducationHistory = data.EducationHistory;
            productData.PersonalSkill = data.PersonalSkill;

            // Update product to JSON file
            SaveProducts(products);

            return productData;
        }

        /// <summary>
        /// Update work experience
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public WorkExperienceModel[] UpdateWorkData(WorkExperienceModel[] data)
        {
            // Get all work experience
            var works = GetAllWorkData().ToDictionary(w => w.Id, w => w);

            // Make sure id in JSON file
            foreach (WorkExperienceModel work in data)
            {
                if (!works.ContainsKey(work.Id)) {
                    return null;
                }

                // Overwrite old data by new data
                works[work.Id].Employer = work.Employer;
                works[work.Id].Title = work.Title;
                works[work.Id].StartDate = work.StartDate;
                works[work.Id].EndDate = work.EndDate;
                works[work.Id].RoleDescription = work.RoleDescription;
            }

            WorkExperienceModel[] updatedWorks = (new List<WorkExperienceModel>(works.Values)).ToArray();

            // Update work experience to JSON file
            SaveWorks(updatedWorks);

            return updatedWorks;
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
        /// Write JSON file
        /// </summary>
        /// <param name="works"></param>
        private void SaveWorks(IEnumerable<WorkExperienceModel> works)
        {
            // Write JSON file
            using (var outputStream = File.Create(WorkExperienceFileName))
            {
                JsonSerializer.Serialize<IEnumerable<WorkExperienceModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    works
                );
            }
        }

    }
}