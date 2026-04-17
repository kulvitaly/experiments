using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Wallets;

public class EditWalletTests : IAsyncLifetime
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
    public async Task EditWallet_ChangesFields()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "Ivanovs");
        var userId = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);
        var walletId = await WalletTestHelpers.AddWallet(client, "Mono", "mono.png", "CREDIT_CARD", userId);

        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($id: UUID!) {
                  editWallet(id: $id, name: "Mono Black", iconUrl: "mono-black.png", type: DEBIT_CARD) {
                    id
                    name
                    iconUrl
                    type
                  }
                }
                """,
            variables = new { id = walletId }
        });

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var wallet = doc.RootElement.GetProperty("data").GetProperty("editWallet");

        wallet.GetProperty("id").GetString().Should().Be(walletId);
        wallet.GetProperty("name").GetString().Should().Be("Mono Black");
        wallet.GetProperty("iconUrl").GetString().Should().Be("mono-black.png");
        wallet.GetProperty("type").GetString().Should().Be("DEBIT_CARD");
    }
}
