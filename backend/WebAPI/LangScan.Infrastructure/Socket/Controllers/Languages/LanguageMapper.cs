using LangScan.Domain.Model.Entities;
using LangScan.Infrastructure.Socket.Controllers.Languages.Dto;
using Riok.Mapperly.Abstractions;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts;

[Mapper]
public partial class LanguageMapper
{
    public partial LanguageDto ToDto(Language domain);
}
