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
        public NewsApiResponse NewsArticles { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task OnGetAsync(string query)
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

    }
}
