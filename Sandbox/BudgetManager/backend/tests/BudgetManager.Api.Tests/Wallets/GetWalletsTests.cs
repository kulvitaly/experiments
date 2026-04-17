using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Wallets;

public class GetWalletsTests : IAsyncLifetime
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
    public async Task Wallets_ByFamily_ReturnsAllWalletsInFamily()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "Ivanovs");
        var otherFamilyId = await WalletTestHelpers.RegisterFamily(client, "Others");
        var alice = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);
        var bob = await WalletTestHelpers.CreateUser(client, "Bob", "bob.png", familyId);
        var outsider = await WalletTestHelpers.CreateUser(client, "Outsider", "x.png", otherFamilyId);

        await WalletTestHelpers.AddWallet(client, "Alice Cash", "c.png", "CASH", alice);
        await WalletTestHelpers.AddWallet(client, "Bob Mono", "m.png", "CREDIT_CARD", bob);
        await WalletTestHelpers.AddWallet(client, "Outsider Cash", "o.png", "CASH", outsider);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                query($familyId: UUID!) {
                  wallets(familyId: $familyId) {
                    name
                    type
                    ownerId
                  }
                }
                """,
            variables = new { familyId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var wallets = doc.RootElement.GetProperty("data").GetProperty("wallets").EnumerateArray().ToList();

        wallets.Select(w => w.GetProperty("name").GetString())
            .Should().BeEquivalentTo(new[] { "Alice Cash", "Bob Mono" });
    }

    [Fact]
    public async Task Wallets_ByFamilyAndUser_FiltersByUser()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "Ivanovs");
        var alice = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);
        var bob = await WalletTestHelpers.CreateUser(client, "Bob", "bob.png", familyId);

        await WalletTestHelpers.AddWallet(client, "Alice Cash", "c.png", "CASH", alice);
        await WalletTestHelpers.AddWallet(client, "Alice Mono", "m.png", "CREDIT_CARD", alice);
        await WalletTestHelpers.AddWallet(client, "Bob Cash", "b.png", "CASH", bob);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                query($familyId: UUID!, $userId: UUID) {
                  wallets(familyId: $familyId, userId: $userId) {
                    name
                    ownerId
                  }
                }
                """,
            variables = new { familyId, userId = alice }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var wallets = doc.RootElement.GetProperty("data").GetProperty("wallets").EnumerateArray().ToList();

        wallets.Should().HaveCount(2);
        wallets.Select(w => w.GetProperty("ownerId").GetString())
            .Should().AllBeEquivalentTo(alice);
        wallets.Select(w => w.GetProperty("name").GetString())
            .Should().BeEquivalentTo(new[] { "Alice Cash", "Alice Mono" });
    }
}
