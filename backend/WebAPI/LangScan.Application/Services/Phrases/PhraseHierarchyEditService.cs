using AutoInterfaceAttributes;
using LangScan.Application.Extensions;
using LangScan.Application.Extensions.Relations;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Model.Entities;
using System.Threading.Tasks;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Requests;

namespace LangScan.Application.Services.Phrases;

[AutoInterface]
internal class PhraseHierarchyEditService(
    IReqAuth reqAuth,
    IAppDbContext db) : IPhraseHierarchyEditService
{
    public async Task<PhraseGroup> PatchPhraseGroup(long id, PhraseGroupPatch body)
    {
        var user = await reqAuth.Login();

        var phraseGroup = await db.PhraseGroups
            .WhereReferences(user)
            .FindByIdOrDefaultAsync(id) ?? throw new PhraseHierarchyNotFound();

        phraseGroup.ApplyPatch(body);

        await db.SaveChangesAsync();

        return phraseGroup;
    }

    public async Task<Phrase> PatchPhrase(long id, PhrasePatch body)
    {
        var user = await reqAuth.Login();

        var phrase = await db.Phrases
            .FindByIdOrDefaultAsync(id) ?? throw new PhraseHierarchyNotFound();

        await AuthorizeOwnership(user, phrase);

        phrase.ApplyPatch(body);

        await db.SaveChangesAsync();

        return phrase;
    }

    private async Task AuthorizeOwnership(User user, Phrase phrase)
    {
        _ = await db.PhraseGroups
            .WhereReferences(user)
            .WhereContains(phrase)
            .ToSingleAsync() ?? throw new PhraseHierarchyNotFound();
    }
}
