using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseDto(
    long Id,
    long IdPhraseGroup,

    [Required]
    string WordKnown,

    [Required]
    string WordLearning,

    [Required]
    string DefinitionKnown,

    [Required]
    string DefinitionLearning
);
