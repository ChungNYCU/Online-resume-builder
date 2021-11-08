using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// A model to represent resume education information
    /// </summary>
    public class EducationModel
    {
        // This is ProductID so that we know who owns this Education record 
        public string ProductID { get; set; }
        // This is Education ID so that we know where it is in Product.EducationList
        public string ID { get; set; }
        // getting the university 
        public string University { get; set; }
        // getting the university location 
        public string Location { get; set; }
        // getting the degree earned in university
        public string Degree { get; set; }
        // getting the major studied in university
        public string Major { get; set; }
        // getting the GPA
        public string GPA { get; set; }
        // getting the starting date for the university
        public string StartDate { get; set; }
        // getting  the ending date for the university
        public string EndDate { get; set; }
    }
}
