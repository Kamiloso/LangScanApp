using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseGroupDto(
    long Id,
    long IdUser,
    long IdLanguageKnown,
    long IdLanguageLearning,

    [Required]
    string ProficiencyLevel,

    [Required]
    string Name,

    [Required]
    string Description);
