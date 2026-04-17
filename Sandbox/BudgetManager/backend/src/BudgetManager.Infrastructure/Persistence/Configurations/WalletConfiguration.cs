using BudgetManager.Domain.Users;
using BudgetManager.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasConversion(id => id.Value, value => new WalletId(value));

        builder.Property(w => w.OwnerId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.IconUrl)
            .IsRequired();

        builder.Property(w => w.Type)
            .HasConversion<string>();

        builder.Property(w => w.IsDeleted)
            .IsRequired();

        builder.Property(w => w.DeletedAt);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(w => w.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(w => w.OwnerId);

        builder.HasIndex(w => new { w.Name, w.IsDeleted }).IsUnique();

        builder.HasQueryFilter(w => !w.IsDeleted);
    }
}
