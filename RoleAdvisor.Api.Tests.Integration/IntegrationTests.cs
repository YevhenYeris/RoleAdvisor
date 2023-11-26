using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace RoleAdvisor.API.Tests.Integration;

public class BasicTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/api/employees")]
    [InlineData("/api/projects")]
    [InlineData("/api/roles")]
    [InlineData("/api/skills")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        var jsonContentType = "application/json; charset=utf-8";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.Equal(jsonContentType, response.Content.Headers.ContentType!.ToString());
    }

    [Theory]
    [InlineData("/api/employees")]
    [InlineData("/api/projects")]
    [InlineData("/api/roles")]
    [InlineData("/api/skills")]
    public async Task Get_EndpointsReturnBadRequestAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        var nonExisingId = "fakeId";
        var badRequestCode = HttpStatusCode.BadRequest;
        var problemJsonContentType = "application/problem+json; charset=utf-8";

        // Act
        var response = await client.GetAsync($"{url}/{nonExisingId}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(badRequestCode, response.StatusCode);
        Assert.Equal(problemJsonContentType, response.Content.Headers.ContentType!.ToString());
    }
}
