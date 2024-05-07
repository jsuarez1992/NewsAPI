using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NEWFLASH.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<NewsApiResponse> GetLatestNewsAsync(string keyword = null)
        {
            var apiKey = _configuration["NewsApi:ApiKey"];
            string apiUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=" + apiKey;
            //For search with keyword like "agriculture" or "farm"
            if (!string.IsNullOrEmpty(keyword))
            {
                apiUrl += "&q=" + keyword;
            }

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            // Set User-Agent header
            request.Headers.Add("User-Agent", "NEWFLASH-NewsClient/1.0");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"News API request failed with status code {response.StatusCode} and content: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore // Ignore null values during deserialization
            });
            return newsApiResponse;
        }

        public async Task<NewsApiResponse> SearchNewsAsync(string query)
        {
            var apiKey = _configuration["NewsApi:ApiKey"];
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://newsapi.org/v2/everything?q={query}&apiKey={apiKey}");

            // Set User-Agent header
            request.Headers.Add("User-Agent", "NEWFLASH-NewsClient/1.0");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"News API request failed with status code {response.StatusCode} and content: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore // Ignore null values during deserialization
            });
            return newsApiResponse;
        }
        public async Task<NewsApiResponse> SearchNewsByExactDateAsync(string query, DateTime startDate)
        {
            var apiKey = _configuration["NewsApi:ApiKey"];
            // Format the date to start at the beginning of the day
            string formattedDate = startDate.ToString("yyyy-MM-dd");

            string apiUrl = $"https://newsapi.org/v2/everything?q={query}&from={formattedDate}&to={formattedDate}&apiKey={apiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "NEWFLASH-NewsClient/1.0");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"News API request failed with status code {response.StatusCode} and content: {await response.Content.ReadAsStringAsync()}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NewsApiResponse>(content);
        }



        // Define your models here
        public class NewsApiResponse
        {
            public string Status { get; set; }
            public int TotalResults { get; set; }
            public List<Article> Articles { get; set; }
        }

        public class Article
        {
            public Source Source { get; set; }
            public string Author { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string UrlToImage { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Content { get; set; }
        }

        public class Source
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
