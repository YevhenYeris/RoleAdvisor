using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using YourWebApiNamespace; // Replace with the actual namespace of your Web API

public class IntegrationTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public IntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_EndpointReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/yourcontroller"); // Replace with your actual endpoint

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

        // You can add more specific assertions based on your API response content
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<YourResponseType>(responseContent);
        Assert.NotNull(responseObject);
        // Add more assertions as needed
    }
}
