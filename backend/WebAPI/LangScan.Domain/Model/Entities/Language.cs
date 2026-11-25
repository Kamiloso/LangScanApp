using LangScan.Domain.Crud;

namespace LangScan.Domain.Model.Entities;

public record Language : ICrudEntity
{
    public long Id { get; set; }

    // Immutable
    public required string Code { get; init; }
}
