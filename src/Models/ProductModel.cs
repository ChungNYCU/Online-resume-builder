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
        public List<AwardModel> AwardList { get; set; } = new List<AwardModel>();
        // getting resume education history 
        public List<EducationModel> EducationList { get; set; } = new List<EducationModel>();
        // getting resume Work Experiences
        public List<WorkExperienceModel> WorkExperiences { get; set; } = new List<WorkExperienceModel>();
        // getting resume personal skill 
        public string PersonalSkill { get; set; }
        // getting resume about me 
        public string AboutMe { get; set; }
        // getting resume rating 
        public int[] Ratings { get; set; }
        // get user password 
        public string Password { get; set; }
        // get Viewed
        public int Viewed { get; set; } = 0;
    }



}