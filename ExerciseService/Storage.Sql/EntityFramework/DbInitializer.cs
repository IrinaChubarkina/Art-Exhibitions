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

        if (!context.Exercises.Any())
        {
            context.Exercises.AddRange(
                new Exercise { Name = "Exercise 1", Description = "Description 1", Duration = TimeSpan.FromSeconds(40) },
                new Exercise { Name = "Exercise 2", Description = "Description 2", Duration = TimeSpan.FromSeconds(50) },
                new Exercise { Name = "Exercise 3", Description = "Description 3", Duration = TimeSpan.FromSeconds(30) });

            context.SaveChanges();
        }
    }
}
