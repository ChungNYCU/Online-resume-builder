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
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        // getting the employer for the work experience
        [StringLength(20, MinimumLength = 3)]
        public string Employer { get; set; }
        // getting the starting date for the work experience
        public string StartDate { get; set; }
        // getting the ending date for the work experience
        public string EndDate { get; set; }
        // getting the description for the work experience
        [StringLength(120, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
