using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Languages.Dto;

public record LanguageDto(
    long Id,

    [Required]
    string Code);
