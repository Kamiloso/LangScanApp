using LangScan.Application.Services.Languages;
using LangScan.Domain.Model.Entities;
using LangScan.Infrastructure.Socket.Controllers.Accounts;
using LangScan.Infrastructure.Socket.Controllers.Languages.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Languages;

[ApiController]
[Route("api/languages")]
public class LanguageController(
    ILanguageService service,
    LanguageMapper mapper) : ControllerExtension
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAllLanguages() => await Run(
        executor: async () =>
        {
            IEnumerable<Language> results = await service.GetAll();
            return results.Select(r => mapper.ToDto(r));
        }
    );
}
