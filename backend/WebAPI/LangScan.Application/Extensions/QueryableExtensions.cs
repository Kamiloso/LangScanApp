using LangScan.Domain.Crud;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereId<T>(this IQueryable<T> self, long id)
        where T : ICrudEntity
        => self.Where(e => e.Id == id);

    public static Task<T?> FindByIdOrDefaultAsync<T>(this IQueryable<T> self, long id)
        where T : ICrudEntity
        => self.WhereId(id).ToSingleAsync();

    public static Task<T?> SelectAgain<T>(this IQueryable<T> self, T entity)
        where T : ICrudEntity
        => self.FindByIdOrDefaultAsync(entity.Id);

    public static Task<T?> ToSingleAsync<T>(this IQueryable<T> self)
        => self.FirstOrDefaultAsync();
}
