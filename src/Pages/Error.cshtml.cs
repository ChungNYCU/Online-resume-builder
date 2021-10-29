using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
//Xinyu added
/// <summary>
/// The Development environment shouldn't be enabled for deployed applications.
/// For local debugging, enable the Development environment by
/// setting the ASPNETCORE_ENVIRONMENT environment variable to
/// evelopment and restarting the app.
/// </summary>
namespace ContosoCrafts.WebSite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets or sets the current operation (<see cref="T:System.Diagnostics.
        /// Activity" />) for the current thread.  This flows across async calls.
        /// </summary>
        /// <returns>The current operation for the current thread.</returns>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}