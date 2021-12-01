using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [JsonPropertyName("img")]
        // getting photo from ID in json
        [RegularExpression(@"^((http|https)://)[a-zA-Z0-9\/_:@=.+?,#%&~-]{2,400}", ErrorMessage = "* Enter a valid photo http URL ")]
        [Required]
        public string Photo { get; set; }

        // getting Linkedin data
        [RegularExpression(@"^(https://www.linkedin.com/in/)[a-zA-Z0-9\/_:@=.+?,#%&~-]{1,300}", ErrorMessage = "* Enter your Linkedin profile URL ")]
        [Required]
        public string LinkedinUrl { get; set; }

        // getting user name
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        // getting user email
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "* Must be an email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // getting user phone number
        [RegularExpression("^[0-9]+$", ErrorMessage = "* Only numbers allowed")]
        [StringLength(15)]
        public string Phone { get; set; }

        // getting user address
        [StringLength(90)]
        public string Address { get; set; }

        // getting user awards 
        public List<AwardModel> AwardList { get; set; } = new List<AwardModel>();

        // getting resume education history 
        public List<EducationModel> EducationList { get; set; } = new List<EducationModel>();

        // getting resume Work Experiences
        public List<WorkExperienceModel> WorkExperienceList { get; set; } = new List<WorkExperienceModel>();
        
        // getting resume personal skill 
        [StringLength(3000, MinimumLength = 3)]
        [Required]
        public string PersonalSkill { get; set; }

        // getting resume about me 
        [StringLength(3000, MinimumLength = 3)]
        [Required]
        public string AboutMe { get; set; }

        // getting resume rating 
        public int[] Ratings { get; set; }

        // get user password 
        [Required]
        public string Password { get; set; }

        // get Viewed
        public int Viewed { get; set; } = 0;
    }
}