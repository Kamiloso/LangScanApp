using System.ComponentModel.DataAnnotations;
using LangScan.Domain.Config;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;

public record AccountDto(
    long Id,

    [EmailAddress]
    [MaxLength(Const.LEN_255)]
    string Email,

    [Range(0, 100)]
    int AnimalHealth,

    [MaxLength(Const.LEN_65535)]
    string AnimalFood);
