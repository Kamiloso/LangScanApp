using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Exceptions.Reasons;
using System.Threading.Tasks;
using LangScan.Domain.Model.Composite;
using LangScan.Domain.Model.Requests;
using LangScan.Application.Extensions;
using System.Linq;
using LangScan.Domain.Model.Ai;
using System.Collections.Generic;

namespace LangScan.Application.Services.Phrases;

[AutoInterface]
internal class PhraseHierarchyGenerateUseCase(
    IReqAuth reqAuth,
    IAiBrain aiBrain,
    IPhraseHierarchyCrudService service,
    IAppDbContext db) : IPhraseHierarchyGenerateUseCase
{
    private IEnumerable<AiResponse>? _aiResponses;

    public async Task<PhraseHierarchy> Execute(PhraseHierarchyGenerate body)
    {
        var user = await reqAuth.Login();

        var langKnown = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangKnown) ?? throw new LanguageNotFound();

        var langLearning = await db.Languages
            .FindByIdOrDefaultAsync(body.IdLangLearning) ?? throw new LanguageNotFound();

        _aiResponses ??= await aiBrain.ProcessTextBlock(new AiRequest(
            langKnown.EnglishName,
            langLearning.EnglishName,
            body.ProficiencyLevel,
            body.TextBlock
        ));

        var phraseGroup = body.DerivePhraseGroup(user);
        var phrases = _aiResponses
            .Select(r => r.DerivePhrase(phraseGroup));

        return await service.Post(new PhraseHierarchy
        {
            PhraseGroup = phraseGroup,
            Phrases = phrases,
        });
    }
}
