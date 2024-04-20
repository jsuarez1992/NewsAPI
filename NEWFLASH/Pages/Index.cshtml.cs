using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEWFLASH.Services;
using System.Threading.Tasks;
using static NEWFLASH.Services.NewsService;

namespace NEWFLASH.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NewsService _newsService;

        // Change the property to use the NewsApiResponse type instead of dynamic
        public NewsApiResponse? NewsArticles { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task OnGetAsync(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    // Perform search based on the query
                    NewsArticles = await _newsService.SearchNewsAsync(query);
                }
                else
                {
                    // Get latest news
                    NewsArticles = await _newsService.GetLatestNewsAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching news from the API.");
                if (ex.Message.Contains("Unauthorized"))
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please ensure your API key is valid and try again.";
                }
                else if (ex.Message.Contains("Too many requests"))
                {
                    TempData["ErrorMessage"] = "Too many requests. Please wait a moment and try again.";
                }
                else if (ex.Message.Contains("Bad Request"))
                {
                    TempData["ErrorMessage"] = "Invalid request. Please check your input and try again.";
                }
                else if (ex.Message.Contains("An unexpected error occurred"))
                {
                    TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while fetching news. Please try again later.";
        
                }
                NewsArticles = null;
            }

        }

    }
}
