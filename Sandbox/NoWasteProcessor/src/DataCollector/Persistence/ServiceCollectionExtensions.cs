using Application.Common.Persistence;
using Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOM360.Application.Common.Behaviours;

namespace Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<SourceDbContext>(options => options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!,
                builder => builder.MigrationsAssembly(typeof(SourceDbContext).Assembly.FullName)))
            .AddScoped<SourceDbContextInitializer>()
            .AddScoped<ISourceDbContext>(provider => provider.GetRequiredService<SourceDbContext>())
            .AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SourceDbContext>())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
}
