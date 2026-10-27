using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class PhraseTable : IEntityTypeConfiguration<Phrase>
{
    public void Configure(EntityTypeBuilder<Phrase> builder)
    {
        builder.ToTable("phrases")
            .HasKey(p => p.Id);

        // Properties

        builder.Property(p => p.IdPhraseGroup)
            .IsRequired();

        builder.Property(p => p.WordKnown)
            .HasMaxLength(Const.LEN_4096)
            .IsRequired();

        builder.Property(p => p.WordLearning)
            .HasMaxLength(Const.LEN_4096)
            .IsRequired();

        builder.Property(p => p.DefinitionKnown)
            .HasMaxLength(Const.LEN_4096)
            .IsRequired();

        builder.Property(p => p.DefinitionLearning)
            .HasMaxLength(Const.LEN_4096)
            .IsRequired();

        // Relations

        builder.HasOne<PhraseGroup>()
            .WithMany()
            .HasForeignKey(p => p.IdPhraseGroup)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
