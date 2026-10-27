using LangScan.Application.Interfaces;
using LangScan.Domain.Model.Entities;
using LangScan.Infrastructure.Persistence.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Frozen;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LangScan.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
{
    private static readonly FrozenSet<Type> DeletionRestrictedTypes = [typeof(Language), typeof(User)];

    public DbSet<User> Users => Set<User>();
    public DbSet<PhraseGroup> PhraseGroups => Set<PhraseGroup>();
    public DbSet<Phrase> Phrases => Set<Phrase>();
    public DbSet<Language> Languages => Set<Language>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserTable());
        modelBuilder.ApplyConfiguration(new PhraseGroupTable());
        modelBuilder.ApplyConfiguration(new PhraseTable());
        modelBuilder.ApplyConfiguration(new LanguageTable());
    }

    public Task<T> UseTransaction<T>(IsolationLevel isolationLevel, Func<Task<T>> func)
    {
        var strategy = Database.CreateExecutionStrategy();

        return strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await Database.BeginTransactionAsync(isolationLevel);

            try
            {
                T result = await func();
                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public Task UseTransaction(IsolationLevel isolationLevel, Func<Task> action)
    {
        return UseTransaction(isolationLevel, async () =>
        {
            await action();
            return 0;
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            Type entityType = entry.Entity.GetType();

            if (entry.State == EntityState.Deleted && DeletionRestrictedTypes.Contains(entityType))
                throw new InvalidOperationException($"Deletion of entity '{entityType}' is strictly forbidden");
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
