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
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        private string WorkExperienceFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "workExperience.json"); }
        }

        public IEnumerable<ProductModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

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
        public ProductModel CreateData()
        {
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

            SaveProducts(dataSet);

            return data;
        }

        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            SaveProducts(newDataSet);

            return data;
        }

        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }

            productData.FullName = data.FullName;
            productData.AboutMe = data.AboutMe;
            productData.Awards = data.Awards;
            productData.LinkedinUrl = data.LinkedinUrl;
            productData.Photo = data.Photo;
            productData.EducationHistory = data.EducationHistory;
            productData.PersonalSkill = data.PersonalSkill;

            SaveProducts(products);

            return productData;
        }

        public WorkExperienceModel[] UpdateWorkData(WorkExperienceModel[] data)
        {
            var works = GetAllWorkData().ToDictionary(w => w.Id, w => w);
            foreach (WorkExperienceModel work in data)
            {
                if (!works.ContainsKey(work.Id)) {
                    return null;
                }
                works[work.Id].Employer = work.Employer;
                works[work.Id].Title = work.Title;
                works[work.Id].StartDate = work.StartDate;
                works[work.Id].EndDate = work.EndDate;
                works[work.Id].RoleDescription = work.RoleDescription;
            }
            WorkExperienceModel[] updatedWorks = (new List<WorkExperienceModel>(works.Values)).ToArray();

            SaveWorks(updatedWorks);

            return updatedWorks;
        }

        public bool UpdatePersonalStatus(string productId, string PersonalStatus)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (productId == null)
                return false;
            if (PersonalStatus == null)
                return false;

            products.First(x => x.Id == productId).PersonalStatus = PersonalStatus;

            SaveProducts(products);

            return true;
        }

        private void SaveProducts(IEnumerable<ProductModel> products)
        {

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

        private void SaveWorks(IEnumerable<WorkExperienceModel> works)
        {

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