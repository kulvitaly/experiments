using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Wallets;

public class AddWalletTests : IAsyncLifetime
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
    public async Task AddWallet_ReturnsWallet()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "TestFamily");
        var userId = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($userId: UUID!) {
                  addWallet(name: "Monobank Black", iconUrl: "mono.png", type: CREDIT_CARD, userId: $userId) {
                    id
                    name
                    iconUrl
                    type
                    ownerId
                  }
                }
                """,
            variables = new { userId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var wallet = doc.RootElement.GetProperty("data").GetProperty("addWallet");

        Guid.Parse(wallet.GetProperty("id").GetString()!).Should().NotBeEmpty();
        wallet.GetProperty("name").GetString().Should().Be("Monobank Black");
        wallet.GetProperty("iconUrl").GetString().Should().Be("mono.png");
        wallet.GetProperty("type").GetString().Should().Be("CREDIT_CARD");
        wallet.GetProperty("ownerId").GetString().Should().Be(userId);
    }

    [Fact]
    public async Task AddWallet_DuplicateName_Fails()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "Ivanovs");
        var userId = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);

        await WalletTestHelpers.AddWallet(client, "Cash", "c.png", "CASH", userId);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($userId: UUID!) {
                  addWallet(name: "Cash", iconUrl: "c2.png", type: CASH, userId: $userId) {
                    id
                  }
                }
                """,
            variables = new { userId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        doc.RootElement.TryGetProperty("errors", out var errors).Should().BeTrue();
        errors.GetArrayLength().Should().BeGreaterThan(0);
    }
}
