using LangScan.Domain.Model;
using Microsoft.EntityFrameworkCore;
using LangScan.Application.Interfaces;

namespace LangScan.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
{
    public DbSet<User> Users => Set<User>();
}
