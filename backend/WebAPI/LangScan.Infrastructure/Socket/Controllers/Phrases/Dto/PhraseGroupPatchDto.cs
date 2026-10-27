using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseGroupPatchDto(
    [Required]
    string ProficiencyLevel,

    [Required]
    string Name,

    [Required]
    string Description);
