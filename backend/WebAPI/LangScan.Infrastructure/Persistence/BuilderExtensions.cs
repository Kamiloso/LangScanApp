using LangScan.Domain.Crud;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangScan.Infrastructure.Persistence;

internal static class BuilderExtensions
{
    public static void PrepareTable<T>(this EntityTypeBuilder<T> builder, string name)
        where T : class, ICrudEntity
    {
        builder.ToTable(name)
            .HasKey(e => e.Id);

        builder.Property<uint>("Version")
            .IsRowVersion();
    }
}
