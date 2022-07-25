namespace Core.Repositories;

using Models;

public interface IExerciseRepository
{
    Task<bool> SaveChanges(CancellationToken cancellationToken);

    Task<List<Exercise>> GetAllExercises(CancellationToken cancellationToken);

    Task<Exercise?> GetExercise(int exerciseId, CancellationToken cancellationToken);

    Task CreateExercise(Exercise exercise, CancellationToken cancellationToken);
}
