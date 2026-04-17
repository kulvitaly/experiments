using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
using BudgetManager.Domain.Wallets;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Common.Interfaces;

/// <summary>
/// Abstraction over the EF Core DbContext exposed to the Application layer.
/// DbSet properties will be added here as entities are introduced.
/// </summary>
public interface IBudgetManagerDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Family> Families { get; }
    DbSet<User> Users { get; }
    DbSet<Wallet> Wallets { get; }
}
