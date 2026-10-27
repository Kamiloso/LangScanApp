using AutoInterfaceAttributes;
using LangScan.Application.Extensions;
using LangScan.Application.Extensions.Entities;
using LangScan.Application.Extensions.Relations;
using LangScan.Application.Interfaces;
using LangScan.Application.SubServices;
using LangScan.Domain.Crud;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Pure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Application.Services.Phrases;

partial interface IPhraseHierarchyCrudService : ICrudService<PhraseHierarchy>;

[AutoInterface]
internal class PhraseHierarchyCrudService(
    IReqAuth reqAuth,
    IAppDbContext db) : IPhraseHierarchyCrudService
{
    public async Task<PhraseHierarchy> Get(long id)
    {
        var user = await reqAuth.Login();

        return await db.PhraseGroups
            .WhereId(id)
            .WhereReferences(user)
            .AssembleHierarchies(db.Phrases)
            .ToSingleAsync() ?? throw new PhraseHierarchyNotFound();
    }

    public async Task<IEnumerable<PhraseHierarchy>> GetAll()
    {
        var user = await reqAuth.Login();

        return await db.PhraseGroups
            .WhereReferences(user)
            .AssembleHierarchies(db.Phrases)
            .ToListAsync();
    }

    public async Task<PhraseHierarchy> Post(PhraseHierarchy body)
    {
        var user = await reqAuth.Login();

        if (!body.ReferencesUser(user))
            throw new InvalidUser();

        if (!body.IsValidForInsertion(isFresh: true))
            throw new InvalidCrudRequest();

        return await db.UseTransaction(IsolationLevel.ReadCommitted, async () =>
        {
            if (!await body.PhraseGroup.ReferencedLanguagesExistAsync(db.Languages))
                throw new LanguageNotFound();

            db.PhraseGroups.Add(body.PhraseGroup);

            await db.SaveChangesAsync();

            body.RecreateChildren();
            db.Phrases.AddRange(body.Phrases);

            await db.SaveChangesAsync();

            return body;
        });
    }

    public async Task Delete(long id)
    {
        var user = await reqAuth.Login();

        await db.PhraseGroups
            .WhereId(id)
            .WhereReferences(user)
            .ExecuteDeleteAsync();
    }
}
