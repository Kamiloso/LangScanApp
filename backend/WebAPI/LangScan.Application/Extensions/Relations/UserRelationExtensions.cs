using LangScan.Domain.Model.Entities;
using System.Linq;

namespace LangScan.Application.Extensions.Relations;

internal static class UserRelationExtensions
{
    public static IQueryable<User> WhereContains(this IQueryable<User> self, PhraseGroup phraseGroup)
        => self.Where(u => u.Id == phraseGroup.IdUser);
}
