using LangScan.Domain.Crud;
using LangScan.Domain.Model.Entities;
using LangScan.Domain.Model.Pure;
using LangScan.Domain.Model.Requests;
using LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;
using Riok.Mapperly.Abstractions;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Mappers;

[Mapper]
public partial class PhraseMapper : ICrudMapper<PhraseHierarchy, PhraseHierarchyDto>
{
    public partial PhraseHierarchyGenerate ToDomain(PhraseHierarchyGenerateDto dto);
    public partial PhraseGroupPatch ToDomain(PhraseGroupPatchDto dto);
    public partial PhrasePatch ToDomain(PhrasePatchDto dto);
    public partial PhraseHierarchy ToDomain(PhraseHierarchyDto dto);
    public partial PhraseHierarchyDto ToDto(PhraseHierarchy domain);
    public partial PhraseGroupDto ToDto(PhraseGroup domain);
    public partial PhraseDto ToDto(Phrase domain);
}
