using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    public class WorkExperienceModel
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
