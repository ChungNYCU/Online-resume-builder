using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string University { get; set; }

        // getting the university location 
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string Location { get; set; }

        // getting the degree earned in university
        [StringLength(20, MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        public string Degree { get; set; }

        // getting the major studied in university
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "* Only English letters allowed")]
        public string Major { get; set; }
        // getting the GPA

        [Required]
        [StringLength(3)]
        [RegularExpression(@"^[0-4]{1}.[0-9]{1}$", ErrorMessage = "* Input number from 0.0 - 4.0")]
        public string GPA { get; set; }

        // getting the starting date for the university
        [Required]
        public string StartDate { get; set; }
        // getting  the ending date for the university

        [Required]
        public string EndDate { get; set; }
    }
}
