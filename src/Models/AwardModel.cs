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
        // getting resume award 
        public string Award { get; set; }
        // getting award issuer
        public string Issuer { get; set; }
        // getting award date 
        public string AwardDate { get; set; }
    }
}

