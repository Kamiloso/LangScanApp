using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class UserTable : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users")
            .HasKey(u => u.Id);

        // Properties

        builder.Property(u => u.Email)
            .HasMaxLength(Const.LEN_255)
            .IsRequired();

        builder.Property(u => u.AnimalHealth)
            .IsRequired();

        builder.Property(u => u.AnimalFood)
            .HasMaxLength(Const.LEN_65535)
            .IsRequired();

        // Indexes

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
