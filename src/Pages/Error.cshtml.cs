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
    /// <summary>
    /// Webpage to handle errors
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // Id of the request when errors occur
        public string RequestId { get; set; }
        // Boolean to determine if Id is null
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        // Logger Error
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
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
            // Get current system diagnostics
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}