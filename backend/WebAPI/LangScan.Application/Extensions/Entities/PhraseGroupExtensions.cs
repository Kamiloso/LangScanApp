using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Application.Extensions.Entities;

public static class PhraseGroupExtensions
{
    public static async Task<bool> ReferencedLanguagesExistAsync(this PhraseGroup self, IQueryable<Language> languages)
    {
        int expectedCount = self.IdLanguageKnown == self.IdLanguageLearning ? 1 : 2;

        int actualCount = await languages.CountAsync(
            l => l.Id == self.IdLanguageKnown || l.Id == self.IdLanguageLearning);

        return actualCount == expectedCount;
    }
}
