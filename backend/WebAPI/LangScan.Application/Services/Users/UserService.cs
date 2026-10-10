using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Domain.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LangScan.Application.Services.Users;

[AutoInterface]
public class UserService(IAppDbContext db) : IUserService
{
    public async Task<User> InsertOrUpdate(string email)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = User.CreateInitial(email);

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        return user;
    }
}
