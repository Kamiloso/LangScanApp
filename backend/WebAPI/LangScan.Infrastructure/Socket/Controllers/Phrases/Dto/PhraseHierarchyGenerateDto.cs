using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseHierarchyGenerateDto(
    [Required]
    string Name,

    [Required]
    string Description,

    long IdLangKnown,
    long IdLangLearning,

    [Required]
    string ProficiencyLevel,

    [Required]
    string TextBlock);
