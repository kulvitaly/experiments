using Domain.RawData;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence;

public interface ISourceDbContext
{
    public DbSet<SourceContent> SourceContents { get; }
}
