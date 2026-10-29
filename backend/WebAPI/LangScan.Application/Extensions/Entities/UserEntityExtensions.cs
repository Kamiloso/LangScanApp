using LangScan.Domain.Model.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan.Application.Extensions.Entities;

public static class UserEntityExtensions
{
    public static IQueryable<User> WhereEmail(this IQueryable<User> self, string email)
        => self.Where(u => u.Email == email);

    public static Task<User?> FindByEmailOrDefaultAsync(this IQueryable<User> self, string email)
        => self.WhereEmail(email).ToSingleAsync();
}
