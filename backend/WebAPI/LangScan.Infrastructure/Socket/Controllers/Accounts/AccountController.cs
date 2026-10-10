using LangScan.Application.Services.Users;
using LangScan.Domain.Model;
using LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using LangScan.Infrastructure.Socket.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts;

[ApiController]
[Route("api/account")]
public class AccountController(
    AccountMapper accountMapper,
    IUserService userService) : ControllerExtension
{
    [HttpPut]
    public async Task<ActionResult<AccountDto>> PutAccount(
        [FromBody] AccountPutDto accountPutDto)
    {
        return await Run(
            executor: async () =>
            {
                User user = await userService.InsertOrUpdate(accountPutDto.Email);
                return accountMapper.ToDto(user);
            },
            result: x => Ok(x)
        );
    }
}
