using System.Reflection;
using Application.Common.Persistence;
using Application.Persistence;
using Domain.RawData;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class SourceDbContext : DbContext, ISourceDbContext, IUnitOfWork
{
    public SourceDbContext(DbContextOptions<SourceDbContext> options) : base(options)
    {
    }

    public DbSet<SourceContent> SourceContents => Set<SourceContent>();

    public Task SaveChanges(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
