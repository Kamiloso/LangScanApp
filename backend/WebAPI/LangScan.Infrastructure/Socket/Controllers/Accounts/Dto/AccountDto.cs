using System.ComponentModel.DataAnnotations;
using LangScan.Domain.Config;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;

public record AccountDto(
    long Id,
    [EmailAddress] [MaxLength(Const.LEN_255)] string Email,

    long AnimalTimestamp,
    [Range(double.MinValue, double.MaxValue)] double Thirst,
    [Range(double.MinValue, double.MaxValue)] double Hunger,
    [Range(double.MinValue, double.MaxValue)] double Boredom,

    long OwnerTimestamp,
    long Money,
    [MaxLength(Const.LEN_65535)] string Food);
