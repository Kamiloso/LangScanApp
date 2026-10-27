using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseHierarchyDto
{
    [Required]
    public required PhraseGroupDto PhraseGroup { get; init; }

    [Required]
    public required IEnumerable<PhraseDto> Phrases
    {
        get;
        init => field = [.. value];
    }
}
