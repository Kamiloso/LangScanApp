using LangScan.Application.Services.Phrases;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Composite;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Attributes;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;
using LangScan.Infrastructure.Socket.Extensions;
using LangScan.Infrastructure.Technology;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases;

[ApiController]
[Route("api/phrase-hierarchies")]
public class PhraseHierarchyController(
    IRequestRetrySystem retry,
    IPhraseHierarchyEditService editService,
    IPhraseHierarchyGenerateUseCase phraseHierarchyGenerate,
    PhraseMapper mapper) : ControllerExtension(retry)
{
    [HttpPatch("phrase-groups/{id}")]
    [SwaggerOperation(Summary = "Update phrase group details")]
    [Returns<PhraseGroupDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    [Throws(HttpStatusCode.NotFound, "Phrase group with the specified ID does not exist or does not belong to the user.")]
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

    [HttpPatch("phrases/{id}")]
    [SwaggerOperation(Summary = "Update phrase details")]
    [Returns<PhraseDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    [Throws(HttpStatusCode.NotFound, "Phrase with the specified ID does not exist or its parent group does not belong to the user.")]
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

    [HttpPost("generate")]
    [SwaggerOperation(Summary = "Generate new phrase hierarchy")]
    [Returns<PhraseHierarchyDto>(HttpStatusCode.OK)]
    [Throws(HttpStatusCode.Unauthorized, "Missing or invalid authentication token.")]
    [Throws(HttpStatusCode.NotFound, "Provided known or learning language ID does not exist.")]
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
