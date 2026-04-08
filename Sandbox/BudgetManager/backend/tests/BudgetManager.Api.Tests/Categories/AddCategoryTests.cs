using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Categories;

public class AddCategoryTests : IAsyncLifetime
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
    public async Task AddIncomeCategory_ReturnsCategory()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await RegisterFamilyAsync(client);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($familyId: UUID!) {
                  addIncomeCategory(name: "Salary", iconUrl: "salary.png", familyId: $familyId) {
                    id
                    name
                    type
                    familyId
                  }
                }
                """,
            variables = new { familyId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var category = doc.RootElement.GetProperty("data").GetProperty("addIncomeCategory");

        Guid.Parse(category.GetProperty("id").GetString()!).Should().NotBeEmpty();
        category.GetProperty("name").GetString().Should().Be("Salary");
        category.GetProperty("type").GetString().Should().Be("INCOME");
        category.GetProperty("familyId").GetString().Should().Be(familyId);
    }

    [Fact]
    public async Task AddExpenseCategory_ReturnsCategory()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await RegisterFamilyAsync(client);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($familyId: UUID!) {
                  addExpenseCategory(name: "Groceries", iconUrl: "groceries.png", familyId: $familyId) {
                    id
                    name
                    type
                    familyId
                  }
                }
                """,
            variables = new { familyId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var category = doc.RootElement.GetProperty("data").GetProperty("addExpenseCategory");

        Guid.Parse(category.GetProperty("id").GetString()!).Should().NotBeEmpty();
        category.GetProperty("name").GetString().Should().Be("Groceries");
        category.GetProperty("type").GetString().Should().Be("EXPENSE");
        category.GetProperty("familyId").GetString().Should().Be(familyId);
    }

    [Fact]
    public async Task IncomeCategories_LoadsFamily()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await RegisterFamilyAsync(client);

        await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($familyId: UUID!) {
                  addIncomeCategory(name: "Salary", iconUrl: "salary.png", familyId: $familyId) {
                    id
                  }
                }
                """,
            variables = new { familyId }
        });

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                query($familyId: UUID!) {
                  incomeCategories(familyId: $familyId) {
                    name
                    family {
                      id
                      name
                    }
                  }
                }
                """,
            variables = new { familyId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var category = doc.RootElement
            .GetProperty("data")
            .GetProperty("incomeCategories")[0];

        category.GetProperty("name").GetString().Should().Be("Salary");

        var family = category.GetProperty("family");
        family.GetProperty("id").GetString().Should().Be(familyId);
        family.GetProperty("name").GetString().Should().Be("TestFamily");
    }

    private static async Task<string> RegisterFamilyAsync(HttpClient client)
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
