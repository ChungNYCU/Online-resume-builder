using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

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

        public IEnumerable<ProductModel> GetProducts()
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

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
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

        public void UpdateName(string productId, string FirstName, string LastName)
        {
            var products = GetProducts();
            int beforeLength;
            int afterLength;

            if (products.First(x => x.Id == productId).FirstName != null && products.First(x => x.Id == productId).LastName != null)
                beforeLength = products.First(x => x.Id == productId).FirstName.Length + products.First(x => x.Id == productId).LastName.Length;
            else
                beforeLength = 0;

            if (FirstName == null || LastName == null)
                return;
            else
                afterLength = FirstName.Length + LastName.Length;

            products.First(x => x.Id == productId).FirstName = FirstName;
            products.First(x => x.Id == productId).LastName = LastName;

            using FileStream outputStream = File.OpenWrite(JsonFileName);
            JsonSerializer.Serialize
            (new Utf8JsonWriter(outputStream, new JsonWriterOptions { SkipValidation = true, Indented = true }), products);
            outputStream.Close();
            if (afterLength < beforeLength)
            {
                string jsonString = File.ReadAllText(JsonFileName);
                string updatedJsonString = jsonString.Substring(0, jsonString.Length - (beforeLength - afterLength));
                File.WriteAllText(JsonFileName, updatedJsonString);
            }
        }
    }
}