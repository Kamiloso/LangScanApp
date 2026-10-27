using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class LanguageTable : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("languages")
            .HasKey(l => l.Id);

        // Properties

        builder.Property(l => l.Code)
            .HasMaxLength(Const.LEN_255);

        // Indexes

        builder.HasIndex(l => l.Code)
            .IsUnique();

        // Data

        builder.HasData([
            new() { Id = 1, Code = "pl" },
            new() { Id = 2, Code = "en" },
            new() { Id = 3, Code = "de" },
            new() { Id = 4, Code = "es" },
        ]);
    }
}
