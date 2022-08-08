namespace Storage.Sql.EntityFramework;

using Core.Models;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void SeedData(AppDbContext context)
    {
        try
        {
            context.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not run migrations: {e.Message}");
        }

        if (!context.Galleries.Any())
        {
            context.Galleries.AddRange(
                new Gallery { Name = "Gallery 1", Address = "Address 1", ContactEmail = "contact1@gmail.com" },
                new Gallery { Name = "Gallery 2", Address = "Address 2", ContactEmail = "contact2@gmail.com" },
                new Gallery { Name = "Gallery 3", Address = "Address 3", ContactEmail = "contact3@gmail.com" });

            context.SaveChanges();
        }
    }
}
