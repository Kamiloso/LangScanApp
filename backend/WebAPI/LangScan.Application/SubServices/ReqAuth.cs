using AutoInterfaceAttributes;
using LangScan.Application.Interfaces;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LangScan.Application.SubServices;

[AutoInterface]
internal class ReqAuth(
    IAuthContext authContext,
    IAppDbContext db) : IReqAuth
{
    public async Task<User> Login()
    {
        if (!authContext.TryGetEmail(out string email))
            throw new AuthorizationRequired();

        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = User.Create(email);

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        return user;
    }
}
