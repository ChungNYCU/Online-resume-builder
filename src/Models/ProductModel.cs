using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
/// <summary>
/// This is our user's resumes control page
/// Allow the viewer to see the profile of the resume
/// </summary>
namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// This class define/represent a candidate resume
    /// </summary>
    public class ProductModel
    {
        // getting resumes id. Id is identification for resume
        public string Id { get; set; }
        // getting resumes status
        public string PersonalStatus { get; set; }
        // getting resume maker
        public string Maker { get; set; }

        // pulling resume from json
        [JsonPropertyName("img")]
        // getting photo from ID in json
        public string Photo { get; set; }
        // getting Linkedin data
        public string LinkedinUrl { get; set; }
        // getting user name
        public string FullName { get; set; }
        // getting user awards 
        public AwardModel Awards { get; set; }
        // getting resume education history 
        public Experience Experiences { get; set; }
        // getting resume experience 
        public List<EducationModel> EducationList { get; set; } = new List<EducationModel>();
        // getting resume personal skill 
        public string PersonalSkill { get; set; }
        // getting resume about me 
        public string AboutMe { get; set; }
        // getting resume rating 
        public int[] Ratings { get; set; }
        // get user password 
        public string Password { get; set; }
    }

    /// <summary>
    /// A model to represent Award information
    /// </summary>
    public class AwardModel
    {
        // getting resume award 
        public string Award { get; set; }
        // getting award issuer
        public string Issuer { get; set; }
        // getting award date 
        public string AwardDate { get; set; }
    }



    /// <summary>
    /// A model to represent experience information
    /// </summary>
    public class Experience
    {
        // getting the work title for the work experience
        public string Title { get; set; }
        // getting the employer for the work experience
        public string Employer { get; set; }
        // getting the starting date for the work experience
        public string StartDate { get; set; }
        // getting the ending date for the work experience
        public string EndDate { get; set; }
        // getting the description for the work experience
        public string Description { get; set; }
    }

}