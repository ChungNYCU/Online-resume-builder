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
        public string Photo { get; set; }
        public string LinkedinUrl { get; set; }
        public string FullName { get; set; }
        public string Awards { get; set; }
        public string EducationHistory { get; set; }
        public string PersonalSkill { get; set; }
        public string AboutMe { get; set; }
        public int[] Ratings { get; set; }
        

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

 
    }
}