using Xunit;
using Moq;
using NEWFLASH.Services;
using Microsoft.Extensions.Configuration;

public class NewsServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly NewsService _newsService;
    private readonly HttpClient _mockHttpClient;

    public NewsServiceTests()
    {
        // Setup Configuration Mock
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["NewsApi:ApiKey"]).Returns("test_api_key");

        // Setup HttpClient Mock using the utility from HttpClientMocks
        string jsonResponse = "{\"status\":\"ok\",\"totalResults\":10,\"articles\":[{\"source\":{\"id\":null,\"name\":\"Example News\"},\"author\":\"Author Name\",\"title\":\"Example Title\",\"description\":\"Example Description\",\"url\":\"http://example.com\",\"urlToImage\":\"http://example.com/image.jpg\",\"publishedAt\":\"2021-05-01T00:00:00Z\",\"content\":\"Example Content\"}]}";
        _mockHttpClient = HttpClientMocks.GetHttpClientWithSuccessResponse(jsonResponse);

        // Instantiate the service with the mocked dependencies
        _newsService = new NewsService(_mockHttpClient, _configurationMock.Object);
    }

    [Fact]
    public async Task GetLatestNewsAsync_ReturnsValidNewsData()
    {
        // Act
        var result = await _newsService.GetLatestNewsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ok", result.Status);
        Assert.Equal(10, result.TotalResults);
        Assert.Single(result.Articles);
        Assert.Equal("Example Title", result.Articles[0].Title);
        Assert.Equal("Author Name", result.Articles[0].Author);
    }
}
