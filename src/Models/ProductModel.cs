using System.Text.Json;
using System.Text.Json.Serialization;
/// <summary>
/// This is our user's resumes control page
/// Allow the viewer to see the profile of the resume
/// </summary>
namespace ContosoCrafts.WebSite.Models
{
    /// getting the personal information from the json
    public class ProductModel
    {
        /// getting resumes id
        public string Id { get; set; }
        /// getting resumes status
        public string PersonalStatus { get; set; }
        /// getting resume maker
        public string Maker { get; set; }

        /// pulling resume from json
        [JsonPropertyName("img")]
        /// getting photo from ID in json
        public string Photo { get; set; }
        /// getting Linkedin data from ID in json
        public string LinkedinUrl { get; set; }
        /// getting user name from ID in json
        public string FullName { get; set; }
        /// getting user awards from ID in json
        public AwardModel Awards { get; set; }
        /// getting resume education history from ID in json
        public Education EducationHistory { get; set; }
        /// getting resume experience from ID in json
        public Experience Experiences { get; set; }
        /// getting resume personal skill from ID in json
        public string PersonalSkill { get; set; }
        /// getting resume about me from ID in json
        public string AboutMe { get; set; }
        /// getting resum rating from ID in json
        public int[] Ratings { get; set; }

    }

    /// <summary>
    /// A model to represent Award information
    /// </summary>
    public class AwardModel
    {
        /// getting resume award from json
        public string Award { get; set; }
        /// getting award issuer from json
        public string Issuer { get; set; }
        /// getting award date from json
        public string AwardDate { get; set; }

    }

    /// <summary>
    /// A model to represent resume education information
    /// </summary>
    public class Education
    {
        /// getting user's universitey from json
        public string University { get; set; }
        /// getting the university lcation from json
        public string Location { get; set; }
        /// getting the degree earned in university
        public string Degree { get; set; }
        /// getting the major studied in university
        public string Major { get; set; }
        /// getting the GPA
        public string GPA { get; set; }
        /// getting the starting date for the university
        public string StartDate { get; set; }
        /// getting  the ending date for the university
        public string EndDate { get; set; }

    }

    /// <summary>
    /// A model to represent experience information
    /// </summary>
    public class Experience
    {
        /// getting the work title for the work experience
        public string Title { get; set; }
        /// getting the employer for the work experience
        public string Employer { get; set; }
        /// getting the starting date for the work experience
        public string StartDate { get; set; }
        /// getting the ending date for the work experience
        public string EndDate { get; set; }
        /// getting the description for the work experience
        public string Description { get; set; }

    }

}