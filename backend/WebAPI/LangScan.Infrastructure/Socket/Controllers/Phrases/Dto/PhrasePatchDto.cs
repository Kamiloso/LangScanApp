using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhrasePatchDto(
    [Required]
    string WordKnown,

    [Required]
    string WordLearning,

    [Required]
    string DefinitionKnown,

    [Required]
    string DefinitionLearning);
