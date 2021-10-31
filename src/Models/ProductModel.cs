using System.Text.Json;
using System.Text.Json.Serialization;
/// <summary>
/// This is our user's resumes control page
/// Allow the viewer to see the profile of the resume
/// </summary>
namespace ContosoCrafts.WebSite.Models
{
    // getting the personal information from the json
    public class ProductModel
    {
        public string Id { get; set; }
        public string PersonalStatus { get; set; }
        public string Maker { get; set; }

        [JsonPropertyName("img")]
        public string Photo { get; set; }
        public string LinkedinUrl { get; set; }
        public string FullName { get; set; }
        public AwardModel Awards { get; set; }
        public string EducationHistory { get; set; }
        public string PersonalSkill { get; set; }
        public string AboutMe { get; set; }
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
    
    /// <summary>
    /// A model to storge Award information
    /// </summary>
    public class AwardModel
    {
        public string Award { get; set; }
        public string AwardDate { get; set; }

        public override string ToString() => JsonSerializer.Serialize<AwardModel>(this);
    }

}