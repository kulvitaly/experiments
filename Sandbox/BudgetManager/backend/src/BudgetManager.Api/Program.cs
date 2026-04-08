using BudgetManager.Application;
using BudgetManager.Infrastructure;
using BudgetManager.Infrastructure.Persistence;
using BudgetManager.Api.GraphQL;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddGraphQl();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.MapGraphQL();

await InitializeDatabaseAsync(app);
app.Run();

static async Task InitializeDatabaseAsync(IHost app)
{
    using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider
        .GetRequiredService<BudgetManagerDbContextInitializer>()
        .InitialiseAsync();
}

public partial class Program;

