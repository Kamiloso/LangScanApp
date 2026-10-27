using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LangScan.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<PhraseGroup> PhraseGroups { get; }
    DbSet<Phrase> Phrases { get; }
    DbSet<Language> Languages { get; }

    Task<T> UseTransaction<T>(IsolationLevel isolationLevel, Func<Task<T>> func);
    Task UseTransaction(IsolationLevel isolationLevel, Func<Task> action);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
