using System.Reflection;
using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
using BudgetManager.Domain.Wallets;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence;

public class BudgetManagerDbContext(DbContextOptions<BudgetManagerDbContext> options)
    : DbContext(options), IBudgetManagerDbContext, IUnitOfWork
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Family> Families { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;

    Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
