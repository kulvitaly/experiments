using System.Reflection;
using BudgetManager.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence;

public class BudgetManagerDbContext(DbContextOptions<BudgetManagerDbContext> options)
    : DbContext(options), IBudgetManagerDbContext, IUnitOfWork
{
    // DbSet<T> properties will be added here as entities are introduced.

    Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
