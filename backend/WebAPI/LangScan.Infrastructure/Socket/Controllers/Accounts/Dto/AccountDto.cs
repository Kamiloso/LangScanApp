using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;

public record AccountDto(
    long Id,

    [Required]
    [EmailAddress]
    string Email,

    int AnimalHealth,

    [Required]
    string AnimalFood);
