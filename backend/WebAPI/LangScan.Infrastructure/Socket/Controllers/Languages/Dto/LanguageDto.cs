using System.ComponentModel.DataAnnotations;
using LangScan.Domain.Config;

namespace LangScan.Infrastructure.Socket.Controllers.Languages.Dto;

public record LanguageDto(
    long Id,

    [MaxLength(Const.LEN_255)]
    string Code);
