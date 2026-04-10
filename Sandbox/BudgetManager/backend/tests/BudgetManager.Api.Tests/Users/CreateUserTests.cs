using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Users;

public class CreateUserTests : IAsyncLifetime
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
    public async Task CreateUser_WithoutFamily_ReturnsUser()
    {
        var client = _fixture.Create<HttpClient>();

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation {
                  createUser(name: "Alice", iconUrl: "alice.png") {
                    id
                    name
                    iconUrl
                    familyId
                  }
                }
                """
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var user = doc.RootElement.GetProperty("data").GetProperty("createUser");

        Guid.Parse(user.GetProperty("id").GetString()!).Should().NotBeEmpty();
        user.GetProperty("name").GetString().Should().Be("Alice");
        user.GetProperty("iconUrl").GetString().Should().Be("alice.png");
        user.GetProperty("familyId").ValueKind.Should().Be(JsonValueKind.Null);
    }

    [Fact]
    public async Task CreateUser_WithFamily_ReturnsFamilyId()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await RegisterFamily(client);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($familyId: UUID!) {
                  createUser(name: "Bob", iconUrl: "bob.png", familyId: $familyId) {
                    id
                    name
                    familyId
                  }
                }
                """,
            variables = new { familyId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var user = doc.RootElement.GetProperty("data").GetProperty("createUser");

        Guid.Parse(user.GetProperty("id").GetString()!).Should().NotBeEmpty();
        user.GetProperty("name").GetString().Should().Be("Bob");
        user.GetProperty("familyId").GetString().Should().Be(familyId);
    }

    private static async Task<string> RegisterFamily(HttpClient client)
    {
        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation {
                  registerFamily(name: "TestFamily") {
                    id
                  }
                }
                """
        });
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement
            .GetProperty("data")
            .GetProperty("registerFamily")
            .GetProperty("id")
            .GetString()!;
    }
}
