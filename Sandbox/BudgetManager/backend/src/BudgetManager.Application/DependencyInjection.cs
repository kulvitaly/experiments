using BudgetManager.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });

        return services;
    }
}
