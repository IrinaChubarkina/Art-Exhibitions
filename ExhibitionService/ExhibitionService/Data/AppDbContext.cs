namespace ExhibitionService.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt)
        : base(opt)
    {
        
    }

    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Exhibition> Exhibitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Gallery>()
            .HasMany(g => g.Exhibitions)
            .WithOne(e=> e.Gallery)
            .HasForeignKey(p => p.GalleryId);

        modelBuilder
            .Entity<Exhibition>()
            .HasOne(e => e.Gallery)
            .WithMany(g => g.Exhibitions)
            .HasForeignKey(e =>e.GalleryId);
    }
}