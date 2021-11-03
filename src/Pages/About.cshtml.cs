using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

/// <summary>
/// Website application introduction page 
/// to display general information of 
/// this web application to users
/// along with all developers' information
/// </summary>
namespace ContosoCrafts.WebSite.Pages
{
    // the data to show in About.cshtml
    public class AboutModel : PageModel
    {
        // logger
        private readonly ILogger<AboutModel> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Should handle data when retrieving information.
        /// Unused due to not requiring data
        /// </summary>
        public void OnGet()
        {
        }
    }
}
