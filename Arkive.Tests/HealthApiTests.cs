using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Arkive.Tests;

public class HealthApiTests
{
    [Fact]
    public async Task GetHealth_ReturnsOk()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();
        var res = await client.GetAsync("/api/health");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}
