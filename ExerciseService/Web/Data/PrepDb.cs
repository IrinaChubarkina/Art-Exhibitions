namespace Web.Data;

using Core.Models;
using Storage.Sql;

public static class PrepDb
{
    public static void PrepareExerciseData(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext context)
    {
        context.Exercises.AddRange(
            new Exercise { Name = "Exercise 1", Description = "Description 1", Duration = TimeSpan.FromSeconds(40) },
            new Exercise { Name = "Exercise 2", Description = "Description 2", Duration = TimeSpan.FromSeconds(50) },
            new Exercise { Name = "Exercise 3", Description = "Description 3", Duration = TimeSpan.FromSeconds(30) });

        context.SaveChanges();
    }
}
