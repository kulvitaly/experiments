using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests.Wallets;

public class DeleteWalletTests : IAsyncLifetime
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
    public async Task DeleteWallet_SoftDeletes_HidesFromWalletsList()
    {
        var client = _fixture.Create<HttpClient>();
        var familyId = await WalletTestHelpers.RegisterFamily(client, "Ivanovs");
        var userId = await WalletTestHelpers.CreateUser(client, "Alice", "alice.png", familyId);
        var keepId = await WalletTestHelpers.AddWallet(client, "Keep", "k.png", "CASH", userId);
        var dropId = await WalletTestHelpers.AddWallet(client, "Drop", "d.png", "CREDIT_CARD", userId);

        var deleteResponse = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($id: UUID!) {
                  deleteWallet(id: $id)
                }
                """,
            variables = new { id = dropId }
        });
        deleteResponse.EnsureSuccessStatusCode();

        using var deleteDoc = JsonDocument.Parse(await deleteResponse.Content.ReadAsStringAsync());
        deleteDoc.RootElement.GetProperty("data").GetProperty("deleteWallet").GetBoolean().Should().BeTrue();

        var listResponse = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                query($familyId: UUID!) {
                  wallets(familyId: $familyId) {
                    id
                    name
                  }
                }
                """,
            variables = new { familyId }
        });
        listResponse.EnsureSuccessStatusCode();

        using var listDoc = JsonDocument.Parse(await listResponse.Content.ReadAsStringAsync());
        var wallets = listDoc.RootElement.GetProperty("data").GetProperty("wallets").EnumerateArray().ToList();

        wallets.Should().HaveCount(1);
        wallets[0].GetProperty("id").GetString().Should().Be(keepId);
        wallets[0].GetProperty("name").GetString().Should().Be("Keep");
    }
}
