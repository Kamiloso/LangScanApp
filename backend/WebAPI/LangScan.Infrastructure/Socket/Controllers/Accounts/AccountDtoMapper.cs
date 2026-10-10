using LangScan.Domain.Model;
using LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;
using Riok.Mapperly.Abstractions;

namespace LangScan.Infrastructure.Socket.Mappers;

[Mapper]
public partial class AccountMapper
{
    public partial User ToDomain(AccountPutDto accountPutDto, long id = 0);

    public partial AccountDto ToDto(User user);
}
