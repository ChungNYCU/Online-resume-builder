using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string PersonalStatus { get; set; }
        public string Maker { get; set; }

        [JsonPropertyName("img")]
        // June Liao modifited "Image" to "Photo"
        public string Photo { get; set; }
        // June Liao modifited "URL" to "LinkedinUrl"
        public string LinkedinUrl { get; set; }
        // June Liao modifited "Title" to "FullName"
        public string FullName { get; set; }
        public string Awards { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }
        

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

 
    }
}