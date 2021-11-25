using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    public class WorkExperienceModel
    {
        // This is ProductID so that we know who owns this WorkExperience record 
        public string ProductID { get; set; }
        // This is WorkExperience ID so that we know where it is in Product.AwardList
        public string ID { get; set; }
        // getting the work title for the work experience

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        public string Title { get; set; }

        // getting the employer for the work experience
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        public string Employer { get; set; }
        // getting the starting date for the work experience

        [Required]
        public string StartDate { get; set; }
        // getting the ending date for the work experience
        [Required]
        public string EndDate { get; set; }
        // getting the description for the work experience
        [Required]
        [StringLength(800, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
