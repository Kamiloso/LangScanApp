using LangScan.Application.Services.Users;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts;

[ApiController]
[Route("api/account")]
public class AccountController(
    IUserService service,
    AccountMapper mapper) : ControllerExtension
{
    [HttpGet]
    public async Task<ActionResult<AccountDto>> GetAccount() => await Run(
        executor: async () =>
        {
            User result = await service.Get();
            return mapper.ToDto(result);
        }
    );

    [HttpPatch]
    public async Task<ActionResult<AccountDto>> PatchAnimalData(
        [FromBody] AccountAnimalPatchDto bodyDto) => await Run(
        executor: async () =>
        {
            UserAnimalPatch body = mapper.ToDomain(bodyDto);
            User result = await service.PatchAnimal(body);
            return mapper.ToDto(result);
        }
    );
}
