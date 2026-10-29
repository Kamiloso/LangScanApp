using LangScan.Application.Services.Users;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Attributes;
using LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts;

[ApiController]
[Route("api/account")]
public class AccountController(
    IRequestRetrySystem retry,
    IUserService service,
    AccountMapper mapper) : ControllerExtension(retry)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get current account data")]
    [Returns<AccountDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    public async Task<ActionResult<AccountDto>> GetAccount() => await Run(
        executor: async () =>
        {
            User result = await service.Get();
            return mapper.ToDto(result);
        }
    );

    [HttpPatch]
    [SwaggerOperation(Summary = "Update animal data")]
    [Returns<AccountDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
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
