namespace Storage.Sql.EntityFramework;

using Core.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt)
        : base(opt)
    {
    }

    public DbSet<Exercise> Exercises { get; set; }
}
