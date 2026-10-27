using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;
using Riok.Mapperly.Abstractions;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts;

[Mapper]
public partial class AccountMapper
{
    public partial AccountDto ToDto(User domain);
    public partial UserAnimalPatch ToDomain(AccountAnimalPatchDto dto);
}
