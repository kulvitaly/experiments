using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BudgetManagerDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")!,
                b => b.MigrationsAssembly(typeof(BudgetManagerDbContext).Assembly.FullName)));

        services.AddScoped<BudgetManagerDbContextInitializer>();
        services.AddScoped<IBudgetManagerDbContext>(
            sp => sp.GetRequiredService<BudgetManagerDbContext>());
        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<BudgetManagerDbContext>());

        return services;
    }
}
