using Domain.RawData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SourceContentConfiguration : IEntityTypeConfiguration<SourceContent>
{
    public void Configure(EntityTypeBuilder<SourceContent> builder)
    {
    }
}
