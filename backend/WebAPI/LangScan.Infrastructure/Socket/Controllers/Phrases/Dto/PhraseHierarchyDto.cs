using System.Collections.Generic;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseHierarchyDto
{
    public required PhraseGroupDto PhraseGroup { get; init; }
    public required IEnumerable<PhraseDto> Phrases
    {
        get;
        init => field = [.. value];
    }
}
