using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangScan.Application.Services.Languages;

[AutoInterface]
internal class LanguageService(
    IAppDbContext db) : ILanguageService
{
    public async Task<IEnumerable<Language>> GetAll()
        => await db.Languages.ToListAsync();
}
