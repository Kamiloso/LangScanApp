using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class LanguageTable : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.PrepareTable("languages");

        // Properties

        builder.Property(l => l.Code)
            .HasMaxLength(Const.LEN_255);

        // Indexes

        builder.HasIndex(l => l.Code)
            .IsUnique();

        // Data

        builder.HasData([
            new() { Id = 1, Code = "pl", EnglishName = "polish" },
            new() { Id = 2, Code = "en", EnglishName = "english" },
            new() { Id = 3, Code = "es", EnglishName = "spanish" },
            new() { Id = 4, Code = "de", EnglishName = "german" },
            new() { Id = 5, Code = "fr", EnglishName = "french" },
            new() { Id = 6, Code = "it", EnglishName = "italian" },
        ]);
    }
}
