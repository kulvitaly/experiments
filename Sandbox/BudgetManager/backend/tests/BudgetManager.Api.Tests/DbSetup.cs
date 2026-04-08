using AutoFixture;
using BudgetManager.Application;
using BudgetManager.Infrastructure;
using BudgetManager.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace BudgetManager.Api.Tests;

public class DbSetup : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var container = new PostgreSqlBuilder("postgres:17-alpine")
            .WithDatabase("budgetmanager")
            .WithUsername("budgetmanager")
            .WithPassword("budgetmanager")
            .Build();

        container.StartAsync().GetAwaiter().GetResult();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = container.GetConnectionString()
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddApplication();
        services.AddInfrastructure(configuration);

        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<BudgetManagerDbContextInitializer>()
            .InitialiseAsync().GetAwaiter().GetResult();

        fixture.Inject(container);
        fixture.Inject(sp.GetRequiredService<IServiceScopeFactory>());
    }
}
