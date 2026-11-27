using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class UserTable : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.PrepareTable("users");

        // Properties

        builder.Property(u => u.Email)
            .HasMaxLength(Const.LEN_255)
            .IsRequired();

        builder.Property(u => u.AnimalTimestamp)
            .IsRequired();

        builder.Property(u => u.Thirst)
            .IsRequired();

        builder.Property(u => u.Hunger)
            .IsRequired();

        builder.Property(u => u.Boredom)
            .IsRequired();

        builder.Property(u => u.OwnerTimestamp)
            .IsRequired();

        builder.Property(u => u.Money)
            .IsRequired();

        builder.Property(u => u.Food)
            .HasMaxLength(Const.LEN_65535)
            .IsRequired();

        // Indexes

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
