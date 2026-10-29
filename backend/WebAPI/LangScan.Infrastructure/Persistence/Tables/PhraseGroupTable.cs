using LangScan.Domain.Config;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence.Tables;

internal class PhraseGroupTable : IEntityTypeConfiguration<PhraseGroup>
{
    public void Configure(EntityTypeBuilder<PhraseGroup> builder)
    {
        builder.PrepareTable("phrase_groups");

        // Properties

        builder.Property(g => g.IdUser)
            .IsRequired();

        builder.Property(g => g.IdLanguageKnown)
            .IsRequired();

        builder.Property(g => g.IdLanguageLearning)
            .IsRequired();

        builder.Property(g => g.ProficiencyLevel)
            .HasMaxLength(Const.LEN_255)
            .IsRequired();

        builder.Property(g => g.Name)
            .HasMaxLength(Const.LEN_255)
            .IsRequired();

        builder.Property(g => g.Description)
            .HasMaxLength(Const.LEN_65535)
            .IsRequired();

        // Relations

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.IdUser)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(g => g.IdLanguageKnown)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(g => g.IdLanguageLearning)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
