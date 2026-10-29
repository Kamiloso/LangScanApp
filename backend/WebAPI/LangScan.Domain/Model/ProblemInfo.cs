namespace LangScan.Domain.Model;

public record ProblemInfo
{
    public required int Status { get; init; }
    public required string Type { get; init; }
    public required string Title { get; init; }
    public required string Detail { get; init; }
    public required string Code { get; init; }
}
