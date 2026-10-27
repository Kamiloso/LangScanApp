using LangScan.Application.Services.Phrases;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Pure;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Mappers;
using LangScan.Infrastructure.Socket.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases;

[ApiController]
[Route("api/phrase-hierarchies")]
public class PhraseHierarchyController(
    IPhraseHierarchyEditService editService,
    IPhraseHierarchyGenerateUseCase phraseHierarchyGenerate,
    PhraseMapper mapper) : ControllerExtension
{
    [HttpPatch]
    [Route("phrase-groups/{id}")]
    public async Task<ActionResult<PhraseGroupDto>> PatchPhraseGroup(
        [FromRoute] long id,
        [FromBody] PhraseGroupPatchDto bodyDto) => await Run(
        executor: async () =>
        {
            PhraseGroupPatch body = mapper.ToDomain(bodyDto);
            PhraseGroup result = await editService.PatchPhraseGroup(id, body);
            return mapper.ToDto(result);
        }
    );

    [HttpPatch]
    [Route("phrases/{id}")]
    public async Task<ActionResult<PhraseDto>> PatchPhrase(
        [FromRoute] long id,
        [FromBody] PhrasePatchDto bodyDto) => await Run(
        executor: async () =>
        {
            PhrasePatch body = mapper.ToDomain(bodyDto);
            Phrase result = await editService.PatchPhrase(id, body);
            return mapper.ToDto(result);
        }
    );

    [HttpPost]
    [Route("generate")]
    public async Task<ActionResult<PhraseHierarchyDto>> GeneratePhraseHierarchy(
        [FromBody] PhraseHierarchyGenerateDto bodyDto) => await Run(
        executor: async () =>
        {
            PhraseHierarchyGenerate body = mapper.ToDomain(bodyDto);
            PhraseHierarchy result = await phraseHierarchyGenerate.Execute(body);
            return mapper.ToDto(result);
        }
    );
}
