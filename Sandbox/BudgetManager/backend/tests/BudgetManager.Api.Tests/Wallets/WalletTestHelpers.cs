using System.Net.Http.Json;
using System.Text.Json;

namespace BudgetManager.Api.Tests.Wallets;

internal static class WalletTestHelpers
{
    public static async Task<string> RegisterFamily(HttpClient client, string name)
    {
        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = """
                mutation($name: String!) {
                  registerFamily(name: $name) {
                    id
                  }
                }
                """,
            variables = new { name }
        });
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement
            .GetProperty("data")
            .GetProperty("registerFamily")
            .GetProperty("id")
            .GetString()!;
    }

    public static async Task<string> CreateUser(HttpClient client, string name, string iconUrl, string? familyId = null)
    {
        var query = familyId is null
            ? """
                mutation($name: String!, $iconUrl: String!) {
                  createUser(name: $name, iconUrl: $iconUrl) { id }
                }
                """
            : """
                mutation($name: String!, $iconUrl: String!, $familyId: UUID!) {
                  createUser(name: $name, iconUrl: $iconUrl, familyId: $familyId) { id }
                }
                """;

        object variables = familyId is null
            ? new { name, iconUrl }
            : new { name, iconUrl, familyId };

        var response = await client.PostAsJsonAsync("/graphql", new { query, variables });
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement
            .GetProperty("data")
            .GetProperty("createUser")
            .GetProperty("id")
            .GetString()!;
    }

    public static async Task<string> AddWallet(HttpClient client, string name, string iconUrl, string type, string userId)
    {
        var response = await client.PostAsJsonAsync("/graphql", new
        {
            query = $$"""
                mutation($userId: UUID!) {
                  addWallet(name: "{{name}}", iconUrl: "{{iconUrl}}", type: {{type}}, userId: $userId) {
                    id
                  }
                }
                """,
            variables = new { userId }
        });
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement
            .GetProperty("data")
            .GetProperty("addWallet")
            .GetProperty("id")
            .GetString()!;
    }
}
