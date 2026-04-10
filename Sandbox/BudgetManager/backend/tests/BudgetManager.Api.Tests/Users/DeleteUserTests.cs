using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Users;

public class DeleteUserTests : IAsyncLifetime
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
    public async Task DeleteUser_RemovesUserFromList()
    {
        var client = _fixture.Create<HttpClient>();
        var userId = await CreateUser(client, "Alice", "alice.png");

        var deleteResponse = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($id: UUID!) {
                  deleteUser(id: $id)
                }
                """,
            variables = new { id = userId }
        });

        deleteResponse.EnsureSuccessStatusCode();

        using var deleteDoc = JsonDocument.Parse(await deleteResponse.Content.ReadAsStringAsync());
        deleteDoc.RootElement.GetProperty("data").GetProperty("deleteUser").GetBoolean().Should().BeTrue();

        var listResponse = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                query {
                  users {
                    id
                  }
                }
                """
        });

        listResponse.EnsureSuccessStatusCode();

        using var listDoc = JsonDocument.Parse(await listResponse.Content.ReadAsStringAsync());
        var users = listDoc.RootElement.GetProperty("data").GetProperty("users");

        users.EnumerateArray()
            .Select(u => u.GetProperty("id").GetString())
            .Should().NotContain(userId);
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
