using Moq;
using Moq.Protected;
using System.Net;

public static class HttpClientMocks
{
    public static HttpClient GetHttpClientWithSuccessResponse(string jsonResponse)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
           .Protected()
           // Setup the PROTECTED method to mock
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>()
           )
           // Prepare the expected response of the mocked HTTP call
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(jsonResponse),
           })
           .Verifiable();

        // Use the handlerMock to create a new HttpClient object
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/"), // Set a dummy BaseAddress since it's required
        };

        return httpClient;
    }

    public static HttpClient GetHttpClientWithFailureResponse(HttpStatusCode statusCode, string errorMessage)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = statusCode,
               Content = new StringContent(errorMessage),
           })
           .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/"),
        };

        return httpClient;
    }
}
