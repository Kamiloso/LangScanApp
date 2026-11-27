using LangScan.Domain.Config;
using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;

public record AccountPatchDto(
    long AnimalTimestamp,
    [Range(double.MinValue, double.MaxValue)] double Thirst,
    [Range(double.MinValue, double.MaxValue)] double Hunger,
    [Range(double.MinValue, double.MaxValue)] double Boredom,

    long OwnerTimestamp,
    long Money,
    [MaxLength(Const.LEN_65535)] string Food);
