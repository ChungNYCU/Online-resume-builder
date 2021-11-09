using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// A model to represent resume Award information
    /// </summary>
    public class AwardModel
    {
        // This is ProductID so that we know who owns this Award record 
        public string ProductID { get; set; }
        // This is Education ID so that we know where it is in Product.AwardList
        public string ID { get; set; }
        // getting resume award 
        public string Award { get; set; }
        // getting award issuer
        public string Issuer { get; set; }
        // getting award date 
        public string AwardDate { get; set; }
    }
}

