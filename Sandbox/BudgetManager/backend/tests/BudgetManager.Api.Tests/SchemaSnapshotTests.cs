using System.Runtime.CompilerServices;
using System.Text;
using BudgetManager.Api.GraphQL;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using IOPath = System.IO.Path;

namespace BudgetManager.Api.Tests;

public class SchemaSnapshotTests
{
    [Fact]
    public async Task Schema_MatchesSnapshot()
    {
        var services = new ServiceCollection();
        services.AddGraphQl();

        var sp = services.BuildServiceProvider();
        var executor = await sp
            .GetRequiredService<IRequestExecutorResolver>()
            .GetRequestExecutorAsync();

        var actualSchema = executor.Schema.ToString();

        var snapshotPath = GetSnapshotPath();

        if (!File.Exists(snapshotPath) ||
            Environment.GetEnvironmentVariable("UPDATE_SCHEMA_SNAPSHOT") == "true")
        {
            Directory.CreateDirectory(IOPath.GetDirectoryName(snapshotPath)!);
            await File.WriteAllTextAsync(snapshotPath, actualSchema, Encoding.UTF8);
            return;
        }

        var expectedSchema = await File.ReadAllTextAsync(snapshotPath);
        Assert.Equal(expectedSchema, actualSchema);
    }

    private static string GetSnapshotPath([CallerFilePath] string sourceFile = "")
        => IOPath.Combine(
            IOPath.GetDirectoryName(sourceFile)!,
            "__snapshots__",
            "schema.graphql");
}
