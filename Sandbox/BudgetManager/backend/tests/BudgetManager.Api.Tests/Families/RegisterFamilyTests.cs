using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Families;

public class RegisterFamilyTests : IAsyncLifetime
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
    public async Task RegisterFamily_PersistsFamily()
    {
        var client = _fixture.Create<HttpClient>();

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation {
                  registerFamily(name: "Smith") {
                    id
                    name
                  }
                }
                """
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var family = doc.RootElement
            .GetProperty("data")
            .GetProperty("registerFamily");

        family.GetProperty("name").GetString().Should().Be("Smith");
        Guid.Parse(family.GetProperty("id").GetString()!).Should().NotBeEmpty();
    }
}
