using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => new CategoryId(value));

        builder.Property(c => c.FamilyId)
            .HasConversion(id => id.Value, value => new FamilyId(value));

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.IconUrl)
            .IsRequired();

        builder.Property(c => c.Type)
            .HasConversion<string>();

        builder.HasOne<Family>()
            .WithMany()
            .HasForeignKey(c => c.FamilyId);

        builder.HasIndex(c => new { c.FamilyId, c.Name })
            .IsUnique();
    }
}
