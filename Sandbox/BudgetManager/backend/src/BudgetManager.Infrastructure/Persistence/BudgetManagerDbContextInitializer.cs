using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Infrastructure.Persistence;

public class BudgetManagerDbContextInitializer(
    ILogger<BudgetManagerDbContextInitializer> logger,
    BudgetManagerDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database migration failed.");
            throw;
        }
    }
}
