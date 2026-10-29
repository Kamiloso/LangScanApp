using AutoInterfaceAttributes;
using LangScan.Application.Extensions.Entities;
using LangScan.Application.Interfaces;
using LangScan.Domain.Exceptions.Reasons;
using LangScan.Domain.Model.Entities;
using System.Data;
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

        return await db.Users.FindByEmailOrDefaultAsync(email)
            ?? await db.UseTransaction(IsolationLevel.Serializable, async () =>
            {
                var reqUser = await db.Users.FindByEmailOrDefaultAsync(email);

                if (reqUser is null)
                {
                    reqUser = User.Create(email);
                    db.Users.Add(reqUser);

                    await db.SaveChangesAsync();
                }

                return reqUser;
            });
    }
}
