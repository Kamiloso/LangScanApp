using LangScan.Domain.Exceptions;

namespace LangScan.Infrastructure.Socket.Other;

public record ProblemDto(
    int Status, string Type, string Title, string Detail)
{
    public static ProblemDto Of(AbortException ex) => new(
        Status: ex.Status,
        Type: "about:blank",
        Title: ex.Title,
        Detail: ex.Detail
    );
}
