using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

/// <summary>
/// Users control privacy, companies ensure protection. Another important
/// distinction between privacy and protection is who is typically in control.
/// For privacy, users can often control how much of their data is shared and
/// with whom. For protection, it is up to the companies handling data to
/// ensure that it remains private.
/// </summary>
namespace ContosoCrafts.WebSite.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
