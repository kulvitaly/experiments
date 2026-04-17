using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.IconUrl)
            .IsRequired();

        builder.Property(u => u.FamilyId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value.HasValue ? new FamilyId(value.Value) : null)
            .IsRequired(false);

        builder.HasOne<Family>()
            .WithMany()
            .HasForeignKey(u => u.FamilyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
