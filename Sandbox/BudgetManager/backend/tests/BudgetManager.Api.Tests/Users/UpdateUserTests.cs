using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Users;

public class UpdateUserTests : IAsyncLifetime
{
    private readonly IFixture _fixture = new Fixture()
        .Customize(new DbSetup())
        .Customize(new TestServerSetup());

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _fixture.Create<PostgreSqlContainer>().DisposeAsync();
    }

    [Fact]
    public async Task UpdateUser_ChangesNameAndIconUrl()
    {
        var client = _fixture.Create<HttpClient>();
        var userId = await CreateUser(client, "Alice", "alice.png");

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($id: UUID!) {
                  updateUser(id: $id, name: "Alice Updated", iconUrl: "alice-v2.png") {
                    id
                    name
                    iconUrl
                  }
                }
                """,
            variables = new { id = userId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var user = doc.RootElement.GetProperty("data").GetProperty("updateUser");

        user.GetProperty("id").GetString().Should().Be(userId);
        user.GetProperty("name").GetString().Should().Be("Alice Updated");
        user.GetProperty("iconUrl").GetString().Should().Be("alice-v2.png");
    }

    private static async Task<string> CreateUser(HttpClient client, string name, string iconUrl)
    {
        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($name: String!, $iconUrl: String!) {
                  createUser(name: $name, iconUrl: $iconUrl) {
                    id
                  }
                }
                """,
            variables = new { name, iconUrl }
        });
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement
            .GetProperty("data")
            .GetProperty("createUser")
            .GetProperty("id")
            .GetString()!;
    }
}
